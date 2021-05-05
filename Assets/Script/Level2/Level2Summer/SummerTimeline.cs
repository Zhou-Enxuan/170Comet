using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SummerTimeline : MonoBehaviour
{
	public static GameObject player; 
	public static GameObject npc01;
	public static GameObject npc02;
	public static GameObject redSoldier;
	public static GameObject greenSoldier;
    public static GameObject rSoldier01;
    public static GameObject gSoldier01;
	public static Animator npc01Anim;
	public static Animator npc02Anim;

	public static GameObject NpcTwoTimeline;

    void Awake() {
        player = GameObject.Find("Player");
        npc01 = GameObject.Find("NpcOne");
        npc02 = GameObject.Find("NpcTwo");
        redSoldier = GameObject.Find("RedSoldier");
        greenSoldier = GameObject.Find("GreenSoldier");
        rSoldier01 = GameObject.Find("RedSoldier01");
        gSoldier01 = GameObject.Find("GreenSoldier01");
        npc01Anim = npc01.GetComponent<Animator>();
        npc02Anim = npc02.GetComponent<Animator>();
        NpcTwoTimeline = GameObject.Find("NpcTwoTimeline");
    }

    void Start() {
        rSoldier01.SetActive(false);
        gSoldier01.SetActive(false);
        NpcTwoTimeline.SetActive(false);
        redSoldier.SetActive(false);
        greenSoldier.SetActive(false);
        TimelineGameManager.GetDirector(NpcTwoTimeline.GetComponent<PlayableDirector>());
        NpcTwoTimeline.SetActive(true);
        TimelineGameManager.isTimeline = true;
        player.GetComponent<BirdOutDoorMovement>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(NpcTwoTimeline.GetComponent<PlayableDirector>().enabled == false) {
            if (!TimelineGameManager.isTimeline) {
                player.GetComponent<BirdOutDoorMovement>().enabled = true;
                LevelLoader.instance.LoadLevel("Level2Summer");
                npc01.SetActive(false);
                npc02.SetActive(false);
                rSoldier01.SetActive(true);
                gSoldier01.SetActive(true);
	        }
	    }
    }

    public void NpcWalk() {
        redSoldier.SetActive(true);
        greenSoldier.SetActive(true);
    }
}
