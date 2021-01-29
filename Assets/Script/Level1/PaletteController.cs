using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteController : MonoBehaviour {
 public int i = 0;
 public float timerout = 0.0f;
 public float timerin = 0.0f;
 public float wait = 1.0f;
 public bool fadeout = false;
 public bool fadein = false;
 public static Image board;
 public static Image board2;
 public static Image board3;
 public static Image board4;
 public static Image board5;
 public static GameObject paper;
 public static GameObject pen;
 public static GameObject pen_paper;
 public static GameObject Rpaper;
 public static GameObject Rpen;
 public static GameObject QuestionMark;
 public static GameObject PickUpHint;
 public int IspickPen = 0;
 public int IspickPaper = 0;
 public static bool isLevel1End = false;
 bool IsBubbleShowed = false;
 bool IsDialogStart = true;

 void OnEnable(){
    board = GameObject.Find("palette1").GetComponent<Image>();
    board2 = GameObject.Find("palette2").GetComponent<Image>();
    board3 = GameObject.Find("palette3").GetComponent<Image>();
    board4 = GameObject.Find("palette4").GetComponent<Image>();
    board5 = GameObject.Find("palette5").GetComponent<Image>();
    pen_paper = GameObject.Find("BubblePenPaper");
    pen =  GameObject.Find("BubblePen");
    paper =  GameObject.Find("BubblePaper");
    Rpen =  GameObject.Find("Pen");
    Rpaper =  GameObject.Find("Paper");
    QuestionMark = GameObject.Find("GirlQMark");
    PickUpHint = GameObject.Find("PickUpHint");
    pen_paper.SetActive(false);
    paper.SetActive(false);
    pen.SetActive(false);
    PickUpHint.SetActive(false);
    QuestionMark.SetActive(true);
 }

 void OnTriggerStay2D(Collider2D collision){
    if (collision.tag == "trigger" && IsDialogStart) {
        PickUpHint.SetActive(true);
        if (Input.GetKeyDown("space")) {
            PickUpHint.SetActive(false);
            Dialog.PrintDialog("Level1 Start"); 
            QuestionMark.SetActive(false);
            IsDialogStart = false;
        }
    }
    if (i < 2 && GameObject.Find("DialogBox") == null) {
        if (collision.tag == "pen") {
            PickUpHint.SetActive(true);
            if (Input.GetKeyDown("space")) {
                Rpen.SetActive(false);
                if (IspickPaper == 1) {
                    Rpaper.SetActive(true);
                    Rpaper.transform.position = Rpen.transform.position;
                    // Debug.Log("changetopen");
                    // Debug.Log(Rpaper.transform.position);
                    i--;
                    IspickPaper = 0;
                }
                IspickPen = 1;
                i++;
                Debug.Log("pen");
            }
        }
        if (collision.tag == "paper") {
            PickUpHint.SetActive(true);
            if (Input.GetKeyDown("space")) {
                //Debug.Log(Rpaper.transform.position);
                Rpaper.SetActive(false);
                if (IspickPen == 1) {
                    Rpen.SetActive(true);
                    Rpen.transform.position = Rpaper.transform.position;
                    //Debug.Log("changetopen");
                    i--;
                    IspickPen = 0;
                }
                IspickPaper = 1;
                i++;
                Debug.Log("paper");
            }
        }
        if (collision.tag == "trigger") {
            if (IspickPaper == 1 || IspickPen == 1) {
                PickUpHint.SetActive(true);
            }
            if (Input.GetKeyDown("space")) {
                if (IspickPaper == 1) {
                    pen_paper.SetActive(false);
                    pen.SetActive(true);
                    PickUpHint.SetActive(false);
                    Rpaper.transform.position = new Vector2(-2.12f, 1f);
                    Rpaper.SetActive(true);
                    Debug.Log("givepaper");
                    IspickPaper = 0;
                }
                else if (IspickPen == 1) {
                    pen_paper.SetActive(false);
                    paper.SetActive(true);
                    PickUpHint.SetActive(false);
                    Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                    Rpen.SetActive(true);
                    Debug.Log("givepen");
                    IspickPen = 0;
                }              
            }
        }
    }
    
    if (i == 2) {
        if (collision.tag == "trigger") {
            PickUpHint.SetActive(true);
                if (Input.GetKeyDown("space")) {
                i ++;
                PickUpHint.SetActive(false);
                paper.SetActive(false);
                pen.SetActive(false);
                if (Rpen.activeSelf) {
                    Rpaper.transform.position = new Vector2(-2.12f, 1f);
                    Rpaper.SetActive(true);
                } else {
                    Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                    Rpen.SetActive(true);
                }
            }
        }
    }
 }

 void FixedUpdate() {
    if (!IsDialogStart && !IsBubbleShowed && GameObject.Find("DialogBox") == null) {
        pen_paper.SetActive(true);
        IsBubbleShowed = true;
    }
    if (i == 3) {
        if (Input.GetKeyDown("space")) {
            fadein = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);;
        }
        if (fadein == true && timerin >= 0 && timerin < wait) {
            timerin += Time.deltaTime;
            board.color = new Color(255,255,255,timerin/wait);
            if (timerin/wait >= 1) {
                fadein = false;
                timerin = 0;
                i++;
            }
        }
    }
    else if(i == 4) {
        if (fadein == false && Input.GetKeyDown("space")) {
            fadeout = true;
        }
        if (fadeout == true && timerout >= 0 && timerout < wait) {
            fadein = true;
            timerout += Time.deltaTime;
            board.color = new Color(1,1,1,1-(timerout/wait));
            if (timerout/wait >= 1) {
                fadeout = false;
                timerout = 0;
            }
        }
        if (fadein == true && timerin >= 0 && timerin < wait) {
            timerin += Time.deltaTime;
            board2.color = new Color(255,255,255,timerin/wait);
            if (timerin/wait >= 1) {
                fadein = false;
                timerin = 0;
                i++;
            }
        }
    }
    else if (i == 5) {
        if (fadein == false && Input.GetKeyDown("space")) {
            fadeout = true;
        }
        if (fadeout == true && timerout >= 0 && timerout < wait) {
            fadein = true;
            timerout += Time.deltaTime;
            board2.color = new Color(1,1,1,1-(timerout/wait));
            if (timerout/wait >= 1) {
                fadeout = false;
                timerout = 0;
            }
        }
        if (fadein == true && timerin >= 0 && timerin < wait) {
            timerin += Time.deltaTime;
            board3.color = new Color(255,255,255,timerin/wait);
            if (timerin/wait >= 1) {
                fadein = false;
                timerin = 0;
                i++;
            }
        }
    }
    else if (i == 6) {
        if (fadein == false && Input.GetKeyDown("space")) {
            fadeout = true;
        }
        if(fadeout == true && timerout >= 0 && timerout < wait) {
            fadein = true;
            timerout += Time.deltaTime;
            board3.color = new Color(1,1,1,1-(timerout/wait));
            if (timerout/wait >= 1) {
                fadeout = false;
                timerout = 0;
            }
        }
        if (fadein == true && timerin >= 0 && timerin < wait) {
            timerin += Time.deltaTime;
            board4.color = new Color(255,255,255,timerin/wait);
            if (timerin/wait >= 1) {
                fadein = false;
                timerin = 0;
                i++;
            }
        }
    }
    else if (i == 7) {
        if (fadein == false && Input.GetKeyDown("space")) {
            fadeout = true;
        }
        if (fadeout == true && timerout >= 0 && timerout < wait) {
            fadein = true;
            timerout += Time.deltaTime;
            board4.color = new Color(1,1,1,1-(timerout/wait));
            if (timerout/wait >= 1) {
                fadeout = false;
                timerout = 0;
            }
        }
        if (fadein == true && timerin >= 0 && timerin < wait) {
            timerin += Time.deltaTime;
            board5.color = new Color(255,255,255,timerin/wait);
            if (timerin/wait >= 1) {
                fadein = false;
                timerin = 0;
                i++;
            }
        }
    }
    else if (i == 8) {
        if (fadein == false && Input.GetKeyDown("space")) {
            fadeout = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }
        if (fadeout == true && timerout >= 0 && timerout < wait) {
            timerout += Time.deltaTime;
            board5.color = new Color(1,1,1,1-(timerout/wait));
            if (timerout/wait >= 1) {
                Dialog.PrintDialog("Level1 End"); 
                isLevel1End = true;
                i++; 
                fadeout = false;
            }
        }
    }
 }

 void OnTriggerExit2D(Collider2D collision) {
    PickUpHint.SetActive(false);
 }

}
