using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallStaff : MonoBehaviour
{
    public static GameObject RhythmGame;
    public static GameObject NoteHolder;
    Vector3 originalPos;

    void Start() {
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        NoteHolder = GameObject.Find("NoteHolder");
        //五线谱在碰sadFace之前不可见
        RhythmGame.SetActive(false);
        //originalPos = new Vector3(RhythmGame.transform.position.x, RhythmGame.transform.position.y, RhythmGame.transform.position.z);
        originalPos = new Vector3(NoteHolder.transform.position.x, NoteHolder.transform.position.y, NoteHolder.transform.position.z);
    }


    //当bird撞上sadFace时触发五线谱
    void OnTriggerStay2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0) {
            if(Input.GetKeyDown("space")){
                //RhythmGame.transform.position = originalPos;
                NoteHolder.transform.position = originalPos;
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false; //stop player control
                GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = true;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GameObject.Find("SadFace").SetActive(false);
                RhythmGame.SetActive(true); //五线谱出现
            }
            
    	}
    }
}
