using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static GameObject NpcTwoFall;
    public static GameObject NoticeMark;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static GameObject Flower; //小花
    public static GameObject RhythmGame; //小花

	void Awake() {
        NpcTwoFall = GameObject.Find("NpcTwoFall");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        RhythmGame = GameObject.Find("RhythmGame");
        Flower = GameObject.Find("Flower");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoFall.SetActive(false);
        Flower.SetActive(false);
    }

    void Update() { 
        //if (NoticeMark.activeSelf && !AutoMovement.isDialoged) {
        if (NoticeMark.activeSelf) {
            Debug.Log("村民來了");
            VillagerTimeline.SetActive(true);
            //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            //完成马的timeline，玩家恢复移动，npc2跌倒+哭
            //if(GameManager.EndTimeline() && GameObject.Find("DialogBox")==null) {
                Debug.Log("跌倒");
                NoticeMark.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;
                NpcTwoFall.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            //}
        }



        //黨小游戲完成時，npc2摔倒消失。小花出現。第二關任務完成
        if(RhythmGame.GetComponent<RhythmScore>().IsGameEnded == true){
            NpcTwoFall.SetActive(false);


            //之後插入拿出花的動畫

            this.GetComponent<SpriteRenderer>().enabled = true;
            Flower.SetActive(true); 
            RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;

            //第二關任務結束
            GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>().isLevel2End = true;
        }

    }

}
