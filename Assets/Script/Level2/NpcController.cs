using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static GameObject NpcTwoFall;
    public static GameObject NoticeMark;    
   	public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花
    public static GameObject Horse;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static bool isPlayerMove = false;

	void Awake() {
        NpcTwoFall = GameObject.Find("NpcTwoFall");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        RhythmGame = GameObject.Find("RhythmGame");
        Flower = GameObject.Find("Flower");
        Horse = GameObject.Find("Horse");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoFall.SetActive(false);
        Flower.SetActive(false);
        Horse.SetActive(false);
    }

    void Update() { 
        //是否完成npc和骑马情节(此为情节1)
        if (!GamePlaySystemManager.isLevel2NpcPlot) {
            if (NoticeMark.activeSelf) {
                isPlayerMove = true;
                //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
                Horse.SetActive(true);
                VillagerTimeline.SetActive(true); //播放cg动画
                GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
                //完成马的timeline，玩家恢复移动，npc2跌倒+哭
                if(GameManager.EndTimeline()){
                    Destroy(GameObject.Find("TimelineCamera"));
                    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
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
            //没完成音游
            if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
                this.GetComponent<SpriteRenderer>().enabled = false;
                NpcTwoFall.SetActive(true);
                Debug.Log("对话后摔倒");
            }
            // 完成音游 结束情节2
            else {
                NpcTwoFall.SetActive(false);


                //之後插入拿出花的動畫

                this.GetComponent<SpriteRenderer>().enabled = true;
                Flower.SetActive(true); 
                RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;

                //第二關冬天任務結束
                GamePlaySystemManager.isLevel2WinterEnd = true;
                Debug.Log("任务结束");
            }

        }
    }

}
