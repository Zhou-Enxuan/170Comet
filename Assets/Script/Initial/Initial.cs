using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Initial : MonoBehaviour
{
    PlayerData thisData;
    void Start()
    {
        string path = Application.persistentDataPath + "/game.fun";
        if (File.Exists(path))
        {
            thisData = SaveSystem.LoadGame();
            GameManager.instance.playerLevel = thisData.playerLevel;
            GameManager.instance.playedLevel = thisData.playedLevel;
        }
    }
    void Update()
    {
        LevelLoader.instance.LoadLevel("Level4Trace");
    }
}
