using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySystemManager : MonoBehaviour
{
    public static bool isLevel1Mission1End = false; //捡东西情节
    public static bool isLevel2NpcPlot = false; //Npc对话至播放跑马动画的情节
    public static bool isLevel2WinterEnd = false; //音游情节
    public static bool isLevel2Flower = false; //有无捡花
    private bool isLevelExit;
    private bool isPass;
    
    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        isLevelExit = false;
        isPass = false;
    }

    // Update is called once per frame
    void Update() {
    	Debug.Log(isLevel1Mission1End);
    	//跳过关卡 按P
    	if (SceneManager.GetActiveScene().name == "Level1") {
    		if (Input.GetKey(KeyCode.P)) {
    			isLevel1Mission1End = true;
    			isPass = true;
    		}
    	}
        if (SceneManager.GetActiveScene().name != "Level1") {
            if (!isLevel1Mission1End) {
        	   GameObject.Find("Player").GetComponent<PaletteController>().enabled = false;
            } 
            else {
                Destroy(GameObject.Find("Player").GetComponent<PaletteController>());
            }
        //in level1
        } else {
            // 首次
            if (!isLevelExit && !isPass) {
        	    GameObject.Find("Player").GetComponent<PaletteController>().enabled = true;
                isLevel1Mission1End = PaletteController.isLevel1End;
            //if mission one end, destroy missionone's gameobject after loading in level1 scene agian再次进入销毁任务1的东西
            }
            else {
                Destroy(GameObject.Find("MissionOne"));
            }
        }

        if (SceneManager.GetActiveScene().name == "Level2") {
            isLevelExit = true;
            isLevel2Flower = FlowerDisappear.isPickFlower;
            if (isLevel2NpcPlot) {
                Destroy(GameObject.Find("NpcPlot"));
                Destroy(GameObject.Find("NpcOne").GetComponent<AutoMovement>());
                // Debug.Log("删除npc对话");
                if (isLevel2WinterEnd && isLevel2Flower) {
                    Destroy(GameObject.Find("Flower"));
                }
            } 
            
        }


    }
}
