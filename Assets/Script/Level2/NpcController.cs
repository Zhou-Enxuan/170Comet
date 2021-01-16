using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static GameObject NpcTwoFall;
    public static GameObject NoticeMark;
    public static GameObject NpcTwoTimeline; //胡子男拿花的timeline
    public static GameObject VillagerTimeline; //骑马过来的timeline

	void Awake() {
        NpcTwoFall = GameObject.Find("NpcTwoFall");
        NoticeMark = GameObject.Find("NoticeMark");
        VillagerTimeline = GameObject.Find("VillagerTimeline");
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
        NoticeMark.SetActive(false);
        VillagerTimeline.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        NpcTwoFall.SetActive(false);
    }

    void Update() { 
        if (NoticeMark.activeSelf && !AutoMovement.isDialoged) {
            VillagerTimeline.SetActive(true);
            //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            //完成马的timeline，玩家恢复移动，npc2跌倒+哭
            if(GameManager.EndTimeline() && GameObject.Find("DialogBox")==null) {
                NoticeMark.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;
                NpcTwoFall.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
}
