//By Huazhen Xu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NpcController : MonoBehaviour
{
    public static GameObject Player; 
    public static GameObject NoticeMark; 
    public static GameObject SadFace;   
   	//public static GameObject Flower; //小花
    //public static GameObject RhythmGame; //小花
    public static GameObject Horse;
    public static GameObject Npc01;
    public static GameObject Npc03;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline
    public static bool isToStartTimeline = false; //开始cg ->automovent.cs调用启动
    public static Animator Npc02Animator;
    public static Animator Npc01Animator;
    //public static Animator Npc02FallAnimator;
    public static GameObject NpcTwoGroup; //npc对话（情景1）中的npc2素材
    public static bool isCameraChanged = false;
    public static bool isLevel2NpcPlot = false;
    public static bool isLevel2WinterEnd = false;
    // bool isDialoged = false;
    Vector2 Npc02OriPos;
	Vector2 Npc02TransPos;
    Camera MainCamera;

	void Awake() {
        Player = GameObject.Find("Player");
        Npc02TransPos = GameObject.Find("NpcTwoPick").GetComponent<Transform>().position;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Npc02OriPos = this.GetComponent<Transform>().position;
        Npc02Animator = this.GetComponent<Animator>();
        Npc01 = GameObject.Find("NpcOne");
        Npc01Animator = Npc01.GetComponent<Animator>();
        Npc03 = GameObject.Find("NpcThree");
        NpcTwoGroup = GameObject.Find("G_NpcTimeline");
        SadFace = GameObject.Find("SadFace");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        // RhythmGame = GameObject.Find("RhythmGame");
        // Flower = GameObject.Find("Flower");
        Horse = GameObject.Find("Horse");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoGroup.SetActive(false);
        // Flower.SetActive(false);
        Horse.SetActive(false);
        SadFace.SetActive(false);
        Npc03.SetActive(false);
        //RhythmGame.SetActive(false);
        Npc01Animator.enabled = false;
        Npc02Animator.enabled = false;
        //Npc02FallAnimator.enabled = false;
        SoundManager.playLv2Bgm(1);
    }

    void Update() {
        if (GameObject.Find("Window") == null) {
            LevelLoader.instance.LoadLevel("Level2Room1");
        }
    //冬天
        //是否完成npc和骑马情节(此为情节1)
        // if (!isLevel2NpcPlot) {
        if (isToStartTimeline) {
            Player.GetComponent<BirdOutDoorMovement>().enabled = false;
            TimelineGameManager.GetDirector(VillagerTimeline.GetComponent<PlayableDirector>());
            VillagerTimeline.SetActive(true); //播放cg动画
            if(VillagerTimeline.GetComponent<PlayableDirector>().enabled == true) {
                if (!TimelineGameManager.isTimeline) {
                    if (!isCameraChanged) {
                        Horse.SetActive(true);
                        NpcTwoGroup.SetActive(true);
                        this.GetComponent<SpriteRenderer>().enabled = false;
                        MainCamera.depth = -3;
                        TimelineGameManager.isTimeline = true;
                    }
                    // else {
                    //     GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
                    //     MainCamera.orthographicSize = 5;
                    // }
                }
            }
            //完成马的timeline，玩家恢复移动，npc2跌倒+哭
            else {
                MainCamera.depth = -1;
                // NpcTransPos(true);
                // SadFace.SetActive(true);
                // this.GetComponent<Transform>().position = Npc02TransPos;
                // this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
                // this.GetComponent<SpriteRenderer>().enabled = true;
                // Npc03.SetActive(true);
                if (!TimelineGameManager.isTimeline) {
                    Player.GetComponent<BirdOutDoorMovement>().enabled = true;
                    //NoticeMark.SetActive(false);
                    // Destroy(GameObject.Find("G_NpcPlot"));
                    // Destroy(Npc01.GetComponent<AutoMovement>());
                    GameManager.instance.Level2WinterNPC();
                    GameManager.instance.StorePlayerPos();
                    SceneManager.LoadScene("Level2WinRhythm");
                }
            }
        }
        //}
        // //完成情节1，未完成情节2
        // else if (!isLevel2WinterEnd) {
        //     Npc01.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/GreenManTurn/A_Npc01Turn04");
        //     Npc03.SetActive(true);
        //     //没完成音游
        //     if (!RhythmGame.GetComponent<RhythmScore>().IsGameEnded) {
        //     	NpcTransPos(true);
        //         Debug.Log("对话后摔倒");
        //     }
        //     // 完成音游 拿出花 结束情节2
        //     else {
        //         if (!isDialoged) {
        //             Player.GetComponent<BirdOutDoorMovement>().enabled = false;
        //             Dialog.PrintDialog("Lv2P1StandUp");
        //             Debug.Log("拿花对话");

        //             //之後插入拿出花的動畫

        //             NpcTransPos(false);
        //             isDialoged = true;
        //         }
        //         if(!GameManager.instance.IsDialogShow()) {
        //             Player.GetComponent<BirdOutDoorMovement>().enabled = true;
        //             RhythmGame.GetComponent<RhythmScore>().IsGameEnded = false;
        //             //第二關冬天任務結束
        //             SadFace.SetActive(false);
        //             GameManager.instance.Level2WinterEnd();
        //             Debug.Log("任务结束");
                    
        //         }
        //     }

        // }
        //完成音游是否拿花--若返回房间出来后的场景
        // else if (isLevel2WinterEnd) {
        //     Npc01.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/GreenManTurn/A_Npc01Turn04");
        //     Npc03.SetActive(true);
        //     //拿了花
        //     if (GamePlaySystemManager.isLevel2Flower) {
        //         this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
        //     }
        //     //没拿花
        //     else {
        //         NpcTransPos(false);
        //     }
        // }
    }


    // public void NpcTransPos(bool isNeedTrans) {
    // 	if (isNeedTrans) {
    //         Flower.SetActive(false); 
    //         SadFace.SetActive(true);
    // 		   this.GetComponent<Transform>().position = Npc02TransPos;
    //         this.GetComponent<SpriteRenderer>().enabled = true;
    //         this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManPick/A_BrownMan_PickHat_05");
    // 	} 
    // 	else {
    // 		this.GetComponent<Transform>().position = Npc02OriPos;
	// 	    this.GetComponent<SpriteRenderer>().enabled = false;
    //         this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
    //         Flower.SetActive(true); 
    // 	}

    // }
    
    //TIMELINE用
    public void CameraTrans(bool isNeedChanged){
        isCameraChanged = isNeedChanged;
        GameObject.Find("Main Camera").GetComponent<CameraSystem>().y_min = 0;
        MainCamera.orthographicSize = 5;
        //Debug.Log("isCamerChanged = "+isCameraChanged);
    }

}
