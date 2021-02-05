using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NpcController : MonoBehaviour
{
    public static GameObject NoticeMark;    
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    public static GameObject Horse;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static bool isPlayerMove = false;
    public static Animator Npc02Animator;
    public static Animator Npc02FallAnimator;
    public static GameObject NpcTwoGroup; //npc对话（情景1）中的npc2素材

	void Awake() {
        Npc02Animator = this.GetComponent<Animator>();
        NpcTwoGroup = GameObject.Find("G_NpcTwoTimeline");
        //Npc02FallAnimator = NpcTwoFall.GetComponent<Animator>();
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
        Npc02Animator.enabled = false;
        //Npc02FallAnimator.enabled = false;
    }

    void Update() { 
        //是否完成npc和骑马情节(此为情节1)
        if (!GamePlaySystemManager.isLevel2NpcPlot) {
            Debug.Log("qima");
            if (NoticeMark.activeSelf) {
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
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
                    this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
                    if (GameObject.Find("DialogBox")==null) {
                        isPlayerMove = false;
                        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
                        GamePlaySystemManager.isLevel2NpcPlot = true;
                        NoticeMark.SetActive(false);
                    }
                }
            }
        }
        //完成情节1，未完成情节2
        else if (!GamePlaySystemManager.isLevel2WinterEnd) {
            Debug.Log("yinyou");
            //没完成音游
            if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
                //NpcTwoFall.SetActive(true);
                Debug.Log("对话后摔倒");
            }
            // 完成音游 结束情节2
            else {
                //NpcTwoFall.SetActive(false);
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");


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

}
