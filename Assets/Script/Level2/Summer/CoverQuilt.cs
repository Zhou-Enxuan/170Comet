using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CoverQuilt : MonoBehaviour
{
    public static Animator Anim; //animation
	public static GameObject Player;
    public static GameObject SpaceHint;
    public static GameObject KeyHint; //盖被子的提示1
    public static GameObject LeaveTip; //离开窗户提示（还没用）
    public static GameObject GirlQMark;
    public static GameObject Quilt;
    public static bool isStart; //开始summer
    public static bool isCovered;
    //[SerializeField] private SpriteAtlas atlas;
    private static SpriteRenderer SR;
    public Sprite NBird2;
    public Sprite NBird4;
    public Sprite NBird6;

    void Start() {
    	Player = GameObject.FindGameObjectWithTag("Player");
    	Player.SetActive(true);
        
        //GetComponent<SpriteRenderer>().sprite = atlas.GetSprite("A_NonBirdQuilt_01");
        Anim = GetComponent<Animator>();
        Quilt = GameObject.Find("Quilt"); //床没有鸟
        LeaveTip = GameObject.Find("LeaveTip");
    	SpaceHint = GameObject.Find("SpaceHint");
        GirlQMark = GameObject.Find("GirlQMark");
        KeyHint = GameObject.Find("KeyHint");
        Quilt.SetActive(true);
        LeaveTip.SetActive(false);
        SpaceHint.SetActive(false);
        GirlQMark.SetActive(true);
        KeyHint.SetActive(false);
        isStart = false;
        isCovered = false;
        SR = Quilt.GetComponent<SpriteRenderer>();
    }

    void Update() {
        //开始盖被子，问号消失
        if (isStart && Input.GetKeyDown("space")) {
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = WBird1;
            //GetComponent<SpriteRenderer>().sprite = atlas.GetSprite("A_Quilt_1");
            SpaceHint.SetActive(false);
            GirlQMark.SetActive(false);
            KeyHint.SetActive(true);
            isStart = false;
        }
        //长按空格出发动画，松开空格从头开始
        if (KeyHint.activeSelf) {  
            if (Input.GetKey("space")) {
                Quilt.SetActive(false);
                Player.SetActive(false);
                switchAnim1();            
            }
            else {
                Player.SetActive(true);
                Quilt.SetActive(true);
                if (Anim.GetBool("Cover1")) {
                    SR.sprite = NBird2;
                } else if(Anim.GetBool("Cover2")) {
                    SR.sprite = NBird4;
                } else if(Anim.GetBool("Cover3")) {
                    SR.sprite = NBird6;
                }
            }
        }
    }


    void switchAnim1(){
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            //按上触发第一段
            if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                Anim.SetBool("Cover1", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按左触发第二段
            else if (Anim.GetBool("Cover1")) {
                Anim.SetBool("Cover1", false);
                Anim.SetBool("Cover2", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按下触发第三段
            else if (Anim.GetBool("Cover2")) {
                Anim.SetBool("Cover2", false);
                Anim.SetBool("Cover3", true); 
            }
            //按右触发接下来的关卡
            else if(Anim.GetBool("Cover3") && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
                KeyHint.SetActive(false);
                LeaveTip.SetActive(true);
                //this.gameObject.GetComponent<SpriteRenderer>().sprite = NBird6;
                Quilt.SetActive(true);
                SR.sprite = NBird6;
                Player.SetActive(true);
                isCovered = true;
                //Destroy(this.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)){
            //按上触发第一段
            if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                //Anim.SetBool("Cover1", true);
                //Debug.Log(Anim.GetBool("Cover3"));
            }
            //按左触发第二段
            else if (Anim.GetBool("Cover1")) {
                Anim.SetBool("Cover1", false);
            }
            //按下触发第三段
            else if (Anim.GetBool("Cover2")) {
                Anim.SetBool("Cover2", false);
                Anim.SetBool("Cover1", true);
            }
            //按右触发接下来的关卡
            else if(Anim.GetBool("Cover3")){
                Anim.SetBool("Cover3", false);
                Anim.SetBool("Cover2", true);
                //KeyHint.SetActive(false);
            }
        }   
    }
    
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && !isCovered) {
            SpaceHint.SetActive(true);
            isStart = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Player" && !isCovered) {
            SpaceHint.SetActive(false);
            isStart = false;
        }
    }

}
