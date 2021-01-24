using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySystemManager : MonoBehaviour
{
	//public string SceneName;
    public bool isLevel1End = false;
    public bool isLevel2End = false;
    //public bool isDialogShown = false;
    private bool isMissionCompleted;
    

    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        isMissionCompleted = false;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name != "Level1") {
        	GameObject.Find("Player").GetComponent<PaletteController>().enabled = false;
        } else {
            if (!isMissionCompleted) {
        	    GameObject.Find("Player").GetComponent<PaletteController>().enabled = true;
                isLevel1End = PaletteController.isLevel1End;
                // if (GameObject.Find("DialogBox") == null) {
                //     isDialogShown = true;
                // } else {
                //     isDialogShown = false;
                // }
            //if mission one end, destroy missionone's gameobject after loading in level1 scene agian再次进入销毁任务1的东西
            } else if (isLevel1End && isLevel2End){
                Debug.Log("level2complete");
                GameObject.Find("Player").GetComponent<PaletteController>().enabled = true;
                Destroy(GameObject.Find("MissionOne"));  
                
            }else if (isLevel1End) {
                Debug.Log("level2complete");
                Destroy(GameObject.Find("MissionOne"));  
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2") {
            isMissionCompleted = true;
        }


    }
}
