using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2RhythmController : MonoBehaviour
{   
    public static GameObject Player; 
    public static GameObject SadFace;   
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    public static GameObject npc02Pick;
    bool isDialoged = false;
 //    Vector2 Npc02OriPos;
	// Vector2 Npc02TransPos;
    
    void Awake()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        Player = GameObject.Find("Player");
        Player.GetComponent<Transform>().position = GameManager.instance.PlayerPos;
        Flower = GameObject.Find("Flower");
        SadFace = GameObject.Find("SadFace");
        RhythmGame = GameObject.Find("RhythmGameUI");
        // Npc02OriPos = this.GetComponent<Transform>().position;
        // Npc02TransPos = GameObject.Find("NpcTwoPick").GetComponent<Transform>().position;
        npc02Pick = GameObject.Find("NpcTwoPick");
        Flower.SetActive(false);
        GameManager.instance.stopMoving = false;
    }
    
    void Update()
    {
        if (!GameManager.instance.isLv2WinterEnd) {
            //没完成音游
            if (!SadFace.GetComponent<CallRhythm>().IsGameEnded) {
                NpcTransPos(true);
                Debug.Log("对话后摔倒");
            }
            // 完成音游 拿出花 结束情节2
            else {
                if (!isDialoged) {
                    Player.GetComponent<BirdOutDoorMovement>().enabled = false;
                    Dialog.PrintDialog("Lv2P1StandUp");
                    Debug.Log("拿花对话");

                    //之後插入拿出花的動畫
                    SadFace.SetActive(false);
                    NpcTransPos(false);
                    isDialoged = true;
                }
                if(!GameManager.instance.IsDialogShow()) {
                    Player.GetComponent<BirdOutDoorMovement>().enabled = true;
                    SadFace.GetComponent<CallRhythm>().IsGameEnded = false;
                    //第二關冬天任務結束
                    
                    GameManager.instance.Level2WinterEnd();
                    Debug.Log("任务结束");
                }
            }
        }
    }
    public void NpcTransPos(bool isNeedTrans) {
    	if (isNeedTrans) {
            // 跌倒
            Flower.SetActive(false); 
            SadFace.SetActive(true);
            this.GetComponent<SpriteRenderer>().enabled = false;
            npc02Pick.SetActive(true);
    	} 
    	else {
            //站起拿花
            npc02Pick.SetActive(false);
    		this.GetComponent<SpriteRenderer>().enabled = true;
            Flower.SetActive(true); 
    	}

    }
}
