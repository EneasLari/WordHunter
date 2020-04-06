using Assets.Scripts.PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.PersistentData.Dictionary;

public class ListController : MonoBehaviour
{
    public GameObject ContentPanel;
    public GameObject ListItemPrefab;
    private ListItemController controller;
    void Start() {
        //if (Globals.PersistentDictionary.DictionaryList==null || Globals.PersistentDictionary.DictionaryList.Count==0) {
        //    GameObject newWord = Instantiate(ListItemPrefab) as GameObject;
        //    controller = newWord.GetComponent<ListItemController>();
        //    controller.wordText.text = "No extra words!";
        //    newWord.transform.SetParent(ContentPanel.transform, false);
        //    newWord.transform.localScale = Vector3.one;
        //}
        //foreach (Word word in Globals.PersistentDictionary.DictionaryList) {
        //    GameObject newWord = Instantiate(ListItemPrefab) as GameObject;
        //    controller = newWord.GetComponent<ListItemController>();
        //    controller.wordText.text = word.Wordstr;
        //    newWord.transform.SetParent(ContentPanel.transform,false);
        //    newWord.transform.localScale = Vector3.one;
        //}
    }

    public void refreshlist() {
        for (int i=0;i<ContentPanel.transform.childCount;i++) {
            Destroy(ContentPanel.transform.GetChild(i).gameObject);
        }
        if (Globals.PersistentDictionary.DictionaryList == null || Globals.PersistentDictionary.DictionaryList.Count == 0) {
            GameObject newWord = Instantiate(ListItemPrefab) as GameObject;
            controller = newWord.GetComponent<ListItemController>();
            controller.wordText.text = "No extra words!";
            newWord.transform.SetParent(ContentPanel.transform, false);
            newWord.transform.localScale = Vector3.one;
        }
        foreach (Word word in Globals.PersistentDictionary.DictionaryList) {
            GameObject newWord = Instantiate(ListItemPrefab) as GameObject;
            controller = newWord.GetComponent<ListItemController>();
            controller.wordText.text = word.Wordstr;
            newWord.transform.SetParent(ContentPanel.transform, false);
            newWord.transform.localScale = Vector3.one;
        }
    }
}
