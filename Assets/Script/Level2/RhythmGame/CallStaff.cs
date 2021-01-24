using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallStaff : MonoBehaviour
{
    public static GameObject RhythmGame;
    //public KeyCode KeyToPress;

    void Start() {
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        //五线谱在碰sadFace之前不可见
        RhythmGame.SetActive(false);
    }

    void Update() {
        
    }

    //当bird撞上sadFace时触发五线谱
    void OnTriggerStay2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0) {
            if(Input.GetKeyDown("space")){
                GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false; //stop player control
                GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = true;
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GameObject.Find("SadFace").SetActive(false);
                RhythmGame.SetActive(true); //五线谱出现
            }
            
    	}
    }
}
