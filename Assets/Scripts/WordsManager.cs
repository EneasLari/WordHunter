using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.PersistentData;
using static Assets.Scripts.PersistentData.Dictionary;

public class WordsManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public PlayerSettings playerSettings;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SubmitWordFromInputField() {
        Debug.Log(inputField.text);
        Word word = new Word();
        word.WordLanguage = Globals.PersistentDictionary.SelectedLanguage;
        word.Wordstr = inputField.text;
        List<Word> dick = Globals.PersistentDictionary.DictionaryList;
        dick.Add(word);
        inputField.text="";
        playerSettings.RefreshSavedList();
        Globals.PersistentDictionary.Serialize();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
