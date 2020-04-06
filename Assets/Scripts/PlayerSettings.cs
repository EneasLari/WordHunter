using Assets.Scripts.PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.Scripts.PersistentData.Dictionary;

public class PlayerSettings : MonoBehaviour
{
    public GameObject PlayerSkins;
    public GameObject Scenes;
    public TextMeshProUGUI wordCollectedDisplay;
    public TextMeshProUGUI numberWordCollectedDisplay;
    public TextMeshProUGUI wordSavedDisplay;

    private User activeuser = null;

    private void Start() {
        activeuser = Globals.UsersManager.Users[Globals.UsersManager.CurrentUserIndex];
        loadWordsSavedList();
        loadWordsCollectedList();
        loadFirstCollectedword();
        loadFirstSavedword();
        loadNumOfWordsCollected();
    }

    public void RefreshSavedList() {
        loadWordsSavedList();
    }

    #region ENABLED_CHILD_CHANGERS

    public void selectNextSkin() {
        for(int i=0;i<PlayerSkins.transform.childCount;i++) {
            GameObject child = PlayerSkins.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy && i != PlayerSkins.transform.childCount-1) {
                child.SetActive(false);
                GameObject nextchild = PlayerSkins.transform.GetChild(i + 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SkinSelected =nextchild.name;
                return;
            } else if (child.activeInHierarchy && i == PlayerSkins.transform.childCount-1) {
                child.SetActive(false);
                GameObject nextchild = PlayerSkins.transform.GetChild(0).gameObject;
                nextchild.SetActive(true);
                activeuser.SkinSelected = nextchild.name;
                return;
            }
        }
    }

    public void selectPreviousSkin() {
        for (int i = 0; i < PlayerSkins.transform.childCount; i++) {
            GameObject child = PlayerSkins.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy && i != 0) {
                child.SetActive(false);
                GameObject nextchild = PlayerSkins.transform.GetChild(i - 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SkinSelected = nextchild.name;
                return;
            } else if (child.activeInHierarchy && i ==0 ) {
                child.SetActive(false);
                GameObject nextchild = PlayerSkins.transform.GetChild(PlayerSkins.transform.childCount - 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SkinSelected = nextchild.name;
                return;
            }
        }
    }

    public void selectNextEvironment() {
        for (int i = 0; i < Scenes.transform.childCount; i++) {
            GameObject child = Scenes.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy && i != Scenes.transform.childCount - 1) {
                child.SetActive(false);
                GameObject nextchild = Scenes.transform.GetChild(i + 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SceneSelected = nextchild.name;
                return;
            } else if (child.activeInHierarchy && i == Scenes.transform.childCount - 1) {
                child.SetActive(false);
                GameObject nextchild = Scenes.transform.GetChild(0).gameObject;
                nextchild.SetActive(true);
                activeuser.SceneSelected=nextchild.name;
                return;
            }
        }
    }

    public void selectPreviousEvironment() {
        for (int i = 0; i < Scenes.transform.childCount; i++) {
            GameObject child = Scenes.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy && i != 0) {
                child.SetActive(false);
                GameObject nextchild = Scenes.transform.GetChild(i - 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SceneSelected = nextchild.name;
                return;
            } else if (child.activeInHierarchy && i == 0) {
                child.SetActive(false);
                GameObject nextchild = Scenes.transform.GetChild(Scenes.transform.childCount - 1).gameObject;
                nextchild.SetActive(true);
                activeuser.SceneSelected = nextchild.name;
                return;
            }
        }
    }

    #endregion

    #region WORDS_COLLECTED_SAVED_PERSISTENTLY

    private List<Word> WordsCollectedList = new List<Word>();
    private int curentIndex = 0;
    public void selectNextCollecteddWord() {
        if (WordsCollectedList.Count-1 > curentIndex) {
            curentIndex++;
        } else {
            curentIndex = 0;
        }
        wordCollectedDisplay.text = WordsCollectedList.Count==0? wordCollectedDisplay.text+"":WordsCollectedList[curentIndex].Wordstr;
    }

    public void selectPreviousCollecteddWord() {
        if (curentIndex > 0) {
            curentIndex--;
        } else {
            curentIndex = WordsCollectedList.Count-1;
        }
        wordCollectedDisplay.text = WordsCollectedList.Count == 0 ? wordCollectedDisplay.text + "" : WordsCollectedList[curentIndex].Wordstr;
    }

    public void loadFirstCollectedword() {
        if (WordsCollectedList == null || WordsCollectedList.Count==0) {
            wordCollectedDisplay.text = "No words found yet";
        } else {
            wordCollectedDisplay.text = WordsCollectedList[0].Wordstr;
        }

    }

    private void loadWordsCollectedList() {
        List<Word> templist = Globals.UsersManager.Users[Globals.UsersManager.CurrentUserIndex].WordsCollected;
        if (templist != null || templist.Count!=0) {
            WordsCollectedList = templist;
        } else {
            Word noword = new Word();
            noword.Wordstr = "No words found yet";
            WordsCollectedList.Add(noword);
        }
    }

    #endregion

    #region WORDS_TO_USE_SAVED_PERSISTENTLY

    private int curentIndex2 = 0;
    private List<Word> WordsSavedList = new List<Word>();

    public void selectNextSavedWord() {
        if (WordsSavedList.Count!=2) { 
        
        }
        if (WordsSavedList.Count - 1 > curentIndex2) {
            curentIndex2++;
        } else {
            curentIndex2 = 0;
        }
        wordSavedDisplay.text = WordsSavedList.Count==0? wordSavedDisplay.text+"": WordsSavedList[curentIndex2].Wordstr;
    }

    public void selectPreviousSavedWord() {
        if (curentIndex2 > 0) {
            curentIndex2--;
        } else {
            curentIndex2 = WordsSavedList.Count - 1;
        }
        wordSavedDisplay.text = WordsSavedList.Count == 0 ? wordSavedDisplay.text + "" : WordsSavedList[curentIndex2].Wordstr;
    }

    private void loadWordsSavedList() {
        List<Word> templist = Globals.PersistentDictionary.DictionaryList;
        Language selectedlang = Globals.PersistentDictionary.SelectedLanguage;
        List<Word> wordsofselectedlanguage = new List<Word>();
        for (int i=0;i<templist.Count;i++) {
            if (templist[i].WordLanguage == selectedlang) {
                wordsofselectedlanguage.Add(templist[i]);
            }
        }
        if (templist != null || templist.Count!=0) {
            WordsSavedList = wordsofselectedlanguage;
        } else {
            Word noword = new Word();
            noword.Wordstr = "No words found yet";
            WordsSavedList.Add(noword);
        }
    }

    public void loadFirstSavedword() {
        if (WordsSavedList == null || WordsSavedList.Count==0) {
            wordSavedDisplay.text = "No words found yet";
        } else {
            wordSavedDisplay.text = WordsSavedList[0].Wordstr;
        }

    }

    public void loadNumOfWordsCollected() {
        if (WordsCollectedList == null || WordsCollectedList.Count == 0) {
            numberWordCollectedDisplay.text = "0";
        } else {
            numberWordCollectedDisplay.text = WordsCollectedList.Count+"";
        }

    }

    #endregion

    #region LEVELS_HANDLERS
    public void Startlevel() {
        print("LETS SAVE BEFORE STARTING PLAY");
        Globals.SerializeAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ExitGame() {
        print("LETS SAVE BEFORE EXIT");
        Globals.SerializeAll();
        Application.Quit();
    }

    #endregion

}
