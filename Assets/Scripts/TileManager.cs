using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public Transform Player;
    public int NumberOfTilesToInstantiate = 18;
    public float TilesLength = 18;
    public GameObject[] TypesOfTilePrefabs = new GameObject[1];

    public int lettersPerTile = 1;
    public GameObject LettersPrefab;
    public bool useEnglish = false;
    public bool useGreek = false;
    public bool useLowerCase = true;

    public int obstaclesPerTile = 1;
    public GameObject ObstaclesPrefab;
    public int ChanceOfObstacle = 20;

    private float NumOfTilesCurrently = 0;

    // Use this for initialization
    void Start() {
        SpawnTiles();
        //NumOfTilesCurrently = +NumberOfTilesToInstantiate;
        //SpawnTile (NumberOfTilesToInstantiate,tileprefab);
        Player.position = transform.position;

    }
    private void InitializeTile(float spawnZ, GameObject prefab) {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + spawnZ);
        GameObject inst;
        inst = Instantiate(prefab, pos, Quaternion.identity);
        inst.transform.SetParent(transform);
        float rand = Random.Range(0.0f, 1.0f);
        InstantiateObstacles(inst, 3, 2);
        InstantiateLetters(inst, 3, 2);
    }
    //Iterate through tiles array and spawns as tiles we have defined next to each other
    private void SpawnTiles() {
         int nextIndex = 0;
         for (int i = 0; i < NumberOfTilesToInstantiate; i++) {
             if (nextIndex < TypesOfTilePrefabs.Length) {
                 InitializeTile((NumOfTilesCurrently * TilesLength), TypesOfTilePrefabs[nextIndex]);
                 nextIndex++;
                 NumOfTilesCurrently++;
             } else {
                 nextIndex = 0;
                 InitializeTile((NumOfTilesCurrently * TilesLength), TypesOfTilePrefabs[nextIndex]);
                 NumOfTilesCurrently++;
             }
         }
    }

    public void TransformGameObject(GameObject obj) { 
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + (NumOfTilesCurrently * TilesLength));
        obj.transform.position = pos;
        NumOfTilesCurrently++;
    }

    public GameObject[] lettersArray = null;
    private GameObject lettersParent = null;
    //here we instantiate the letters for one tile gameobject
    public void InstantiateLetters(GameObject gameobj, int numberofrowsForProbs, int spacebetween) {
        //find the object that holds the appropriate letters gameobjects
        if (lettersParent == null) {
            if (useEnglish) {
                if (useLowerCase) {
                    lettersParent = LettersPrefab.transform.Find("English").Find("lowercase").gameObject;
                } else {
                    lettersParent = LettersPrefab.transform.Find("English").Find("uppercase").gameObject;
                }
            } else if (useGreek) {
                if (useLowerCase) {
                    lettersParent = LettersPrefab.transform.Find("Greek").Find("lowercase").gameObject;
                } else {
                    lettersParent = LettersPrefab.transform.Find("Greek").Find("uppercase").gameObject;
                }
            }
            
        }
        //take tha children game object and pass each element in an gameobject array
        else if (lettersParent != null) {
            int numofchildren = lettersParent.transform.childCount;
            // print("CHILDREN " + numofchildren);
            if (lettersArray == null) {
                lettersArray = new GameObject[numofchildren];
            }
            if (lettersArray[0] == null) {
                for (int j = 0; j < numofchildren; j++) {
                    lettersArray[j] = lettersParent.transform.GetChild(j).gameObject;
                }
            }
            //spawn letters game object randomply
            int i = 0;
            int posY = 2;
            float posZ = gameobj.transform.position.z - TilesLength / 2;//Iam not sure yet

            while (i < lettersPerTile) {
                int letterindex = Random.Range(0, numofchildren);
                //the x position must have an offset(0.25) because of the pivot point not being in the center
                float posX = Random.Range(-(numberofrowsForProbs / 2), (numberofrowsForProbs / 2) + 1)-0.25f;
                Vector3 position = new Vector3(posX * spacebetween, posY, posZ);
                GameObject instance;
                instance = Instantiate(lettersArray[letterindex], position, Quaternion.identity);
                instance.transform.Rotate(0, 180, 0); //Rotate the letter So we can see it right
                instance.transform.SetParent(gameobj.transform);
                if (instance) { 
                
                }
                posZ = posZ + TilesLength / lettersPerTile;
                i++;
            }

        }
        
    }

    //here we instantiate the obstacles for one tile gameobject
    private GameObject[] obstacleArray = null;
    private GameObject obstaclesParent = null;
    public void InstantiateObstacles(GameObject gameobj, int numberofrowsForProbs, int spacebetween) {
        //print("LETS FIIILL THA GAMEOBJECT= " + gameobj.name);
        //find the object that holds the appropriate letters gameobjects
        int propability = Random.Range(0, 100);
        if (propability > ChanceOfObstacle) {
            return;
        }
        if (obstaclesParent == null) {
            obstaclesParent = ObstaclesPrefab;//parent gameobject that holds the obstacles as child gameobject
        }
        //take tha children game object and pass each element in an gameobject array
        else if (obstaclesParent != null) {
            int numofchildren = obstaclesParent.transform.childCount;
            // print("CHILDREN " + numofchildren);
            if (obstacleArray == null) {
                obstacleArray = new GameObject[numofchildren];
            }
            if (obstacleArray[0]==null) {
                for (int j = 0; j < numofchildren; j++) {
                    obstacleArray[j] = obstaclesParent.transform.GetChild(j).gameObject;
                }
            }
            //print("ARRAY==== " + obstacleArray[0].name);
            //spawn letters game object randomply
            int i = 0;
            int posY = 2;
            float posZ = gameobj.transform.position.z - TilesLength / 2;//Iam not sure yet

            while (i < obstaclesPerTile) {
                int letterindex = Random.Range(0, numofchildren);
                float posX = Random.Range(-(numberofrowsForProbs / 2), (numberofrowsForProbs / 2) + 1);

                Vector3 position = new Vector3(posX * spacebetween, posY, posZ);
                GameObject instance;
                instance = Instantiate(obstacleArray[letterindex], position, Quaternion.identity);
                //instance.transform.Rotate(0, 180, 0); //Rotate the letter So we can see it right
                instance.transform.SetParent(gameobj.transform);
                posZ = posZ + TilesLength / obstaclesPerTile;
                //print("next " + posZ);
                i++;
            }

        }
    }

    public void InstantiateCoins() { 
    
    }
    // Update is called once per frame
    private float timer = 0.0f;
    void Update() {
        //SpawnTiles();
        timer += Time.deltaTime;
        float timeInsecs =timer % 60;
        
        if (ChanceOfObstacle!=100 && timeInsecs>5) {
            //print(timeInsecs);
            print(ChanceOfObstacle);
            ChanceOfObstacle++;
            timer = 0;
        }
    }

}
