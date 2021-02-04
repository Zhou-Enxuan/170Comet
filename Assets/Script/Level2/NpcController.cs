using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NpcController : MonoBehaviour
{
    public static GameObject NoticeMark; 
    public static GameObject SadFace;   
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    public static GameObject Horse;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static bool isToStartTimeline = false; //开始cg ->automovent.cs调用启动
    public static bool isPlayerMove = false;
    public static Animator Npc02Animator;
    public static Animator Npc02FallAnimator;
    public static GameObject NpcTwoGroup; //npc对话（情景1）中的npc2素材
    Vector2 Npc02OriPos;
	Vector2 Npc02TransPos;

	void Awake() {
        Npc02TransPos = GameObject.Find("NpcTwoPick").GetComponent<Transform>().position;
        Npc02OriPos = this.GetComponent<Transform>().position;
        Npc02Animator = this.GetComponent<Animator>();
        NpcTwoGroup = GameObject.Find("G_NpcTimeline");
        SadFace = GameObject.Find("SadFace");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        RhythmGame = GameObject.Find("RhythmGame");
        Flower = GameObject.Find("Flower");
        Horse = GameObject.Find("Horse");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoGroup.SetActive(false);
        Flower.SetActive(false);
        Horse.SetActive(false);
        SadFace.SetActive(false);
        //RhythmGame.SetActive(false);
        Npc02Animator.enabled = false;
        //Npc02FallAnimator.enabled = false;
    }

    void Update() { 
        //是否完成npc和骑马情节(此为情节1)
        if (!GamePlaySystemManager.isLevel2NpcPlot) {
            if (isToStartTimeline) {
                isPlayerMove = true;
                //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
                GameManager.ins.GetDirector(VillagerTimeline.GetComponent<PlayableDirector>());
                VillagerTimeline.SetActive(true); //播放cg动画
                if(VillagerTimeline.GetComponent<PlayableDirector>().enabled == true) {
                    if (!GameManager.isTimeline) {
                        Horse.SetActive(true);
                        NpcTwoGroup.SetActive(true);
                        this.GetComponent<SpriteRenderer>().enabled = false;;
                        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
                        GameManager.isTimeline = true;
                    }
                }
                //完成马的timeline，玩家恢复移动，npc2跌倒+哭
                else {
                    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
                	NpcTransPos(true);
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    if (GameObject.Find("DialogBox")==null) {
                        isPlayerMove = false;
                        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
                        GamePlaySystemManager.isLevel2NpcPlot = true;
                        //NoticeMark.SetActive(false);
                        isToStartTimeline = false;
                        Npc02Animator.enabled = false;
                    }
                }
            }
        }
        //完成情节1，未完成情节2
        else if (!GamePlaySystemManager.isLevel2WinterEnd) {
            //没完成音游
            if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
            	NpcTransPos(true);
                SadFace.SetActive(true);
                Debug.Log("对话后摔倒");
            }
            // 完成音游 结束情节2
            else {
                //NpcTwoFall.SetActive(false);
                NpcTransPos(false);


                //之後插入拿出花的動畫

                //this.GetComponent<SpriteRenderer>().enabled = true;
                Flower.SetActive(true); 
                RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;

                //第二關冬天任務結束
                GamePlaySystemManager.isLevel2WinterEnd = true;
                Debug.Log("任务结束");
            }

        }
    }

    public void NpcTransPos(bool isNeedTrans) {
    	if (isNeedTrans) {
    		this.GetComponent<Transform>().position = Npc02TransPos;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
    	} 
    	else {
    		this.GetComponent<Transform>().position = Npc02OriPos;
		    this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/A_Npc02Flower");
    	}

    }

}
