using Assets.Scripts.PersistentData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitilizeSavedSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerSkins;
    public GameObject scene;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Coins;
    void Awake()
    {
        User activeUser = Globals.UsersManager.Users[Globals.UsersManager.CurrentUserIndex];
        //bool initializationDone = false;
        for (int i = 0; i < playerSkins.transform.childCount; i++) {
            //if (activeUser.SkinSelected == "Default") {
            //    playerSkins.transform.GetChild(0).gameObject.SetActive(true);
            //    initializationDone = true;
            //}
            if (playerSkins.transform.GetChild(i).name == activeUser.SkinSelected) {
                playerSkins.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                playerSkins.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        //initializationDone = false;
        for (int i = 0; i < scene.transform.childCount; i++) {
            //if (activeUser.SceneSelected == "Default") {
            //    scene.transform.GetChild(0).gameObject.SetActive(true);
            //    initializationDone = true;
            //}
            if (scene.transform.GetChild(i).name == activeUser.SceneSelected) {
                scene.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                scene.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        Score.text = Score.text + activeUser.Score;
        Coins.text = Coins.text + activeUser.Coins;
    }

}
