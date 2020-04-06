using Assets.Scripts.PersistentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.Scripts.PersistentData.Dictionary;

public class LettersGame : MonoBehaviour
{

    public string CurrentWord;
    public int score = 0;
    public int lifes = 3;
    public int coins = 0;
    public string letterCollected;
    public Language SelectedLanguage;
    public enum Language { Greek, English}

    public LetterCase SelectedLetterCase;
    public enum LetterCase { None, LowerCase, UpperCase, Capitalize }
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI letterText;
    public TextMeshProUGUI coinsText;
    public GameObject LosePanel;
    public GameObject InGamePanel;

    private User _activeUser = null;
    private Dictionary _dictionary = null;

    private int PositionOfLetterToCollect = 0;
    // Start is called before the first frame update
    void Start() {
        _activeUser = Globals.UsersManager.Users[Globals.UsersManager.CurrentUserIndex];
        _dictionary = Globals.PersistentDictionary;

        if (SelectedLanguage == Language.English) {
            GlobalDictionary.setVocabulary("English");
        }
        else if (SelectedLanguage == Language.Greek) {
            GlobalDictionary.setVocabulary("Greek");
        }
        foreach (Word item in _dictionary.DictionaryList) {
            GlobalDictionary.Vocabulary.Add(item.Wordstr);
        }
        getNextWord();
    }

    void OnApplicationQuit() {
        Globals.SerializeAll();
    }

    public void getNextWord() {
        int index = Random.Range(0, GlobalDictionary.Vocabulary.Count);
        CurrentWord = GlobalDictionary.Vocabulary[index];
        if (SelectedLetterCase == LetterCase.LowerCase) {
            CurrentWord = CurrentWord.ToLower();
        } else if (SelectedLetterCase == LetterCase.LowerCase) {
            CurrentWord = CurrentWord.ToUpper();
        } else if (SelectedLetterCase==LetterCase.Capitalize) {
            CurrentWord.Trim();
            char firstLetter = CurrentWord[0];
            CurrentWord=CurrentWord.Substring(1);//Substring of the initial string without the first character
            CurrentWord = firstLetter.ToString().ToUpper()+CurrentWord;
        }
    }

    public string getNextCharacter(string c) {
        if (PositionOfLetterToCollect < CurrentWord.Length - 1 && c.Equals(CurrentWord[PositionOfLetterToCollect] + "(Clone)")) {
            letterCollected = letterCollected + CurrentWord[PositionOfLetterToCollect];
            PositionOfLetterToCollect++;
            return "CorrectLetter";
        } else if (PositionOfLetterToCollect == CurrentWord.Length - 1 && c.Equals(CurrentWord[PositionOfLetterToCollect] + "(Clone)")) {
            letterCollected = letterCollected + CurrentWord[PositionOfLetterToCollect];
            PositionOfLetterToCollect = 0;
            Word newWordCollected = new Word();
            newWordCollected.WordLanguage = _dictionary.SelectedLanguage;
            newWordCollected.Wordstr = letterCollected;
            _activeUser.WordsCollected.Add(newWordCollected);
            _activeUser.Score = _activeUser.Score + score;
            _activeUser.Coins = _activeUser.Coins + coins;
            return "WordFinished";
        } else {
            return "Wrong";
        }
    }

    public void OnLoseLifes() {
        if (lifes==0 && !LosePanel.activeSelf) {
            LosePanel.SetActive(true);
            InGamePanel.SetActive(false);
            PauseGame();
        }
    }

    public void ReplayLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PauseGame() {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame() {
        Time.timeScale = 1;
        //enable the scripts again
    }
    // Update is called once per frame
    void Update() {
        scoreText.text =score+"";
        wordText.text = CurrentWord;
        lifesText.text = "" + lifes;
        letterText.text = letterCollected;
        coinsText.text = "" + coins;
        OnLoseLifes();

    }
}
