using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    PlayerData thisData;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/game.fun";
        if (File.Exists(path))
        {
            thisData = SaveSystem.LoadGame();
            GameManager.instance.playerLevel = thisData.playerLevel;
            GameManager.instance.playedLevel = thisData.playedLevel;
        }

        if(GameManager.instance.playedLevel > 0)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level2Winter");
    }

    public void continueGame()
    {

    }
}
