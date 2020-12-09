using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteController : MonoBehaviour
{

 public int i = 0;
 public float timerout = 0.0f;
 public float timerin = 0.0f;
 public float wait = 1.0f;
 public bool fadeout = false;
 public bool fadein = false;
 public Image board;
 public Image board2;
 public Image board3;
 public Image board4;
 public Image board5;
 public GameObject paper;
 public GameObject pen;
 public GameObject pen_paper;
 public int IspickPen = 0;
 public int IspickPaper = 0;

 void Awake(){
    paper.SetActive(false);
    pen.SetActive(false);
 }


 private void OnTriggerStay2D(Collider2D collision){
     if(i < 2){
   if(collision.tag == "pen" && Input.GetKeyDown("space")){
             Destroy(collision.gameObject);
             IspickPen = 1;
             i++;
             Debug.Log("pen");
         }
    if(collision.tag == "paper" && Input.GetKeyDown("space")){
             Destroy(collision.gameObject);
             IspickPaper = 1;
             i++;
             Debug.Log("paper");
         }

    if(collision.tag == "trigger" && Input.GetKeyDown("space")){
        if(IspickPaper==1){
             pen_paper.SetActive(false);
             pen.SetActive(true);
             Debug.Log("givepaper");
        }else{
            pen_paper.SetActive(false);
             paper.SetActive(true);
             Debug.Log("givepen");
        }              
  }
     }
    


  if(i == 2){
   if(collision.tag == "trigger" && Input.GetKeyDown("space")){
             i ++;
             
         }
  }
 }

  void Update(){
        if(i == 3){
            if(Input.GetKeyDown("space")){
                fadein = true;
            }
            if(fadein == true && timerin >= 0 && timerin < wait){
                timerin += Time.deltaTime;
                board.color = new Color(255,255,255,timerin/wait);
                if(timerin/wait >= 1){
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }else if(i == 4){
            if(fadein == false && Input.GetKeyDown("space")){
                fadeout = true;
            }
            if(fadeout == true && timerout >= 0 && timerout < wait){
                fadein = true;
                timerout += Time.deltaTime;
                board.color = new Color(1,1,1,1-(timerout/wait));
                if(timerout/wait >= 1){
                    fadeout = false;
                    timerout = 0;
                }
            }
            if(fadein == true && timerin >= 0 && timerin < wait){
                timerin += Time.deltaTime;
                board2.color = new Color(255,255,255,timerin/wait);
                if(timerin/wait >= 1){
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }else if(i == 5){
            if(fadein == false && Input.GetKeyDown("space")){
                fadeout = true;
            }
            if(fadeout == true && timerout >= 0 && timerout < wait){
                fadein = true;
                timerout += Time.deltaTime;
                board2.color = new Color(1,1,1,1-(timerout/wait));
                if(timerout/wait >= 1){
                    fadeout = false;
                    timerout = 0;
                }
            }
            if(fadein == true && timerin >= 0 && timerin < wait){
                timerin += Time.deltaTime;
                board3.color = new Color(255,255,255,timerin/wait);
                if(timerin/wait >= 1){
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }else if(i == 6){
            if(fadein == false && Input.GetKeyDown("space")){
                fadeout = true;
            }
            if(fadeout == true && timerout >= 0 && timerout < wait){
                fadein = true;
                timerout += Time.deltaTime;
                board3.color = new Color(1,1,1,1-(timerout/wait));
                if(timerout/wait >= 1){
                    fadeout = false;
                    timerout = 0;
                }
            }
            if(fadein == true && timerin >= 0 && timerin < wait){
                timerin += Time.deltaTime;
                board4.color = new Color(255,255,255,timerin/wait);
                if(timerin/wait >= 1){
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }else if(i == 7){
            if(fadein == false && Input.GetKeyDown("space")){
                fadeout = true;
            }
            if(fadeout == true && timerout >= 0 && timerout < wait){
                fadein = true;
                timerout += Time.deltaTime;
                board4.color = new Color(1,1,1,1-(timerout/wait));
                if(timerout/wait >= 1){
                    fadeout = false;
                    timerout = 0;
                }
            }
            if(fadein == true && timerin >= 0 && timerin < wait){
                timerin += Time.deltaTime;
                board5.color = new Color(255,255,255,timerin/wait);
                if(timerin/wait >= 1){
                    fadein = false;
                    timerin = 0;
                    i++;
                }
            }
        }else if (i == 8){
            if(fadein == false && Input.GetKeyDown("space")){
                fadeout = true;
            }
            if(fadeout == true && timerout >= 0 && timerout < wait){
                timerout += Time.deltaTime;
                board5.color = new Color(1,1,1,1-(timerout/wait));
                if(timerout/wait >= 1){
                    Dialog.PrintDialog("Room1"); 
                    i++; 
                    fadeout = false;
                }
            }
        }
    }


}