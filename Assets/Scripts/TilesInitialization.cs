using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesInitialization : MonoBehaviour {
    public LettersGame LettersgameGameobject;
    public GameObject[] TypesOfTilesPrefabs;

    private GameObject[] _lettersArray = null;
    public GameObject[] LettersArray { 
        get {
            if (_lettersArray == null) {
                InitializeLettersArray();              
            }
            return _lettersArray;
        }
        private set { _lettersArray = value; }
    }
    public int NumberOfTilesToInstantiate = 0;
    public int TilesLength = 18;

    public int NumOfTilesCurrently;

    private void Start() {
        SpawnTiles();
        //InitializeLettersArray();
    }

    private void SpawnTiles() {
        int numberOfTypesOfTiles = TypesOfTilesPrefabs.Length;
        int nextIndex = Random.Range(0, numberOfTypesOfTiles);
        for (int i = 0; i < NumberOfTilesToInstantiate; i++) {
            InstantiateTileinZaxis((NumOfTilesCurrently * TilesLength), TypesOfTilesPrefabs[nextIndex]);
            nextIndex = Random.Range(0, numberOfTypesOfTiles);
            NumOfTilesCurrently++;
        }
    }

    private void InstantiateTileinZaxis(float ZaxisPosition, GameObject prefab) {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZaxisPosition);
        GameObject inst;
        inst = Instantiate(prefab, pos, Quaternion.identity);
        inst.transform.SetParent(transform);
        float rand = Random.Range(0.0f, 1.0f);
    }

    private void InitializeLettersArray() {
        Transform LettersParent = transform.Find("LettersParent");
        if (LettersgameGameobject.SelectedLanguage == LettersGame.Language.English) {
            LettersParent = LettersParent.Find("English");
        } else if (LettersgameGameobject.SelectedLanguage == LettersGame.Language.Greek) {
            LettersParent = LettersParent.Find("Greek");
        }
        if (LettersParent != null) {
            if (LettersgameGameobject.SelectedLetterCase == LettersGame.LetterCase.LowerCase) {
                LettersParent = LettersParent.Find("LowerCase");
            } else if (LettersgameGameobject.SelectedLetterCase == LettersGame.LetterCase.UpperCase) {
                LettersParent = LettersParent.Find("UpperCase");
            }
            if (LettersParent != null) {
                _lettersArray = new GameObject[LettersParent.childCount];
                for (int i=0;i<LettersParent.childCount;i++) {
                    _lettersArray[i] = LettersParent.GetChild(i).gameObject;
                }
            }
        }

    }

    public void TransformGameObject(GameObject obj) {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + (NumOfTilesCurrently * TilesLength));
        obj.transform.position = pos;
        NumOfTilesCurrently++;
    }
}
