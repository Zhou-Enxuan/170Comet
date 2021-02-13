using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2RhythmController : MonoBehaviour
{   
    public static GameObject Player; 
    public static GameObject SadFace;   
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    bool isDialoged = false;
    Vector2 Npc02OriPos;
	Vector2 Npc02TransPos;
    
    void Awake()
    {
        Player = GameObject.Find("Player");
        Player.GetComponent<Transform>().position = GameManager.instance.PlayerPos;
        Flower = GameObject.Find("Flower");
        SadFace = GameObject.Find("SadFace");
        RhythmGame = GameObject.Find("RhythmGame");
        Npc02OriPos = this.GetComponent<Transform>().position;
        Npc02TransPos = GameObject.Find("NpcTwoPick").GetComponent<Transform>().position;
        Flower.SetActive(false);
    }
    
    void Update()
    {
        if (!GameManager.instance.isLv2WinterEnd) {
            //没完成音游
            if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
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

                    NpcTransPos(false);
                    isDialoged = true;
                }
                if(!GameManager.instance.IsDialogShow()) {
                    Player.GetComponent<BirdOutDoorMovement>().enabled = true;
                    RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;
                    //第二關冬天任務結束
                    SadFace.SetActive(false);
                    GameManager.instance.Level2WinterEnd();
                    Debug.Log("任务结束");
                }
            }
        }
    }
    public void NpcTransPos(bool isNeedTrans) {
    	if (isNeedTrans) {
            Flower.SetActive(false); 
            SadFace.SetActive(true);
    		this.GetComponent<Transform>().position = Npc02TransPos;
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
    	} 
    	else {
    		this.GetComponent<Transform>().position = Npc02OriPos;
		    this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
            Flower.SetActive(true); 
    	}

    }
}
