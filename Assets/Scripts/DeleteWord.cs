using Assets.Scripts.PersistentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteWord : MonoBehaviour
{
    // Start is called before the first frame update
    public void deleteWord() {
        string wordtodelete = gameObject.transform.parent.Find("word").GetComponent<Text>().text;
        List<Dictionary.Word> list = Globals.PersistentDictionary.DictionaryList;
        list.Remove(list.Find(x => x.Wordstr.Equals(wordtodelete)));
        ListController gameobj = gameObject.transform.parent.parent.GetComponent<ListController>();
        gameobj.refreshlist();
        //Destroy(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
