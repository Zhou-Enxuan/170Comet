using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            GameManager.instance.updateLevelData(1);
            SaveSystem.SaveGame();
        }
        if(SceneManager.GetActiveScene().name == "Level2Winter")
        {
            GameManager.instance.updateLevelData(2);
            SaveSystem.SaveGame();
        }
        if(SceneManager.GetActiveScene().name == "Level2SummerRoom")
        {
            GameManager.instance.updateLevelData(3);
            SaveSystem.SaveGame();
        }
    }
}
