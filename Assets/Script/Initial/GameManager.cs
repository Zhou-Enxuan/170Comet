using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
    public bool stopMoving = false;
    public bool isLv2Npc { get; private set; }
    public bool isLv2WinterEnd { get; private set; }
	public bool isLv2Flower { get; private set; }
    public bool islv2SummerNewsEnd { get; private set; }
    public bool islv2FallGlassEnd { get; private set; }
    public Vector2 PlayerPos { get; private set; }
    int index;
    public int playerLevel = -1;
    public int playedLevel = -1;
    public int bgmNum = 0;

    // Start is called before the first frame update
    private void Awake() {
        isLv2Npc = false;
        isLv2WinterEnd = false;
        isLv2Flower = false;
        islv2SummerNewsEnd = false;
        islv2FallGlassEnd = false;

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
        
    }
    
    public void StorePlayerPos() {
        PlayerPos = GameObject.Find("Player").GetComponent<Transform>().position;
    }
    
    public void StorePlayerLoc(Vector2 position){
        PlayerPos = position;
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

    public void GiveFlower(){
        instance.isLv2Flower = false;
    }

    public void NewsEnd(){
        instance.islv2SummerNewsEnd = true;
    }
    
    public void GlassEnd() {
        islv2FallGlassEnd = true;
    }
    
    public bool IsDialogShow() {
        if (GameObject.Find("DialogBox") == null) {
            return false;
        } 
        else {
            return true;
        }
    }

    public void updateLevelData(int level_num)
    {
        playerLevel = level_num;
        if(playedLevel < level_num)
        {
            playedLevel = level_num;
        }
    }
    
    // //GirlQuestion.cs调用
    // public void Level2WinFlower(){
    //     //IN LV2ROOM1
    //     if (instance.isLv2WinterEnd) {
    //         GirlQuestion.isRoomStart = true;
    //         if (instance.isLv2Flower) {
    //             GirlQuestion.isRoomFlower = true;
    //         } else {
    //             GirlQuestion.isRoomFlower = false;
    //         }
    //     }
    // }
    // //Lv2R1Window.cs
    // public int Level2RoomTrans() {
    //     if (!instance.isLv2Npc) {
    //         index = 2; // "Level2Winter"
    //     } 
    //     else if (!isLv2WinterEnd) {
    //         index = 3; // "Level2WinRhythm"
    //     }
    //     else if (!isLv2Flower){
    //         index = 5; // "Level2WinFlower"
    //     }
    //     return index;
    // }
    // public void Level2WinterStart() {
    //     if (isLv2Npc) {
    //         NpcController.isLevel2NpcPlot = true;
    //         Destroy(GameObject.Find("G_NpcPlot"));
    //         Destroy(GameObject.Find("NpcOne").GetComponent<AutoMovement>());
    //     }
    //     if (isLv2WinterEnd) {
    //          NpcController.isLevel2WinterEnd = true;
    //     }

    // }
}
