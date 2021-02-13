using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
    private bool isLv2Npc = false;
    private bool isLv2WinterEnd = false;
	private bool isLv2Flower = false;
    string NextSceneName;

    // Start is called before the first frame update
    private void Awake() {
    	if (instance != null) {
    		GameObject.Destroy(instance);
    	}
    	else {
    		instance = this;
    		DontDestroyOnLoad(this);
    	}
    }

    // Update is called once per frame
    void Update()
    {
        //IN LV2ROOM1
        if (instance.isLv2WinterEnd) {
            GirlQuestion.isRoomStart = true;
            if (instance.isLv2Flower) {
                GirlQuestion.isRoomFlower = true;
            } else {
                GirlQuestion.isRoomFlower = false;
            }
        }
    }
    public void Level2WinterStart() {
        if (isLv2Npc) {
            NpcController.isLevel2NpcPlot = true;
            Destroy(GameObject.Find("G_NpcPlot"));
            Destroy(GameObject.Find("NpcOne").GetComponent<AutoMovement>());
        }
        if (isLv2WinterEnd) {
             NpcController.isLevel2WinterEnd = true;
        }

    }
    public void Level2WinterNPC() {
        instance.isLv2Npc = true;
    }
    public void Level2WinterEnd() {
        instance.isLv2WinterEnd = true;
    }

    //FlowerDisappear.cs调用
    public void GetFlower(){
        instance.isLv2Flower = true;
    }

    public bool IsDialogShow() {
        if (GameObject.Find("DialogBox") == null) {
            return false;
        } 
        else {
            return true;
        }
    }
}
