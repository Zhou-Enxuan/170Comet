using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteController : MonoBehaviour
{

 public int i = 0;

 public GameObject board;
 public GameObject board2;
 public GameObject board3;
 public GameObject board4;
 public GameObject board5;
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
                board.SetActive(true);
                i++;
            }
        }else if(i == 4){
            if(Input.GetKeyDown("space")){
                board.SetActive(false);
                board2.SetActive(true);
                i++;
            }
        }else if(i == 5){
            if(Input.GetKeyDown("space")){
                board2.SetActive(false);
                board3.SetActive(true);
                i++;
            }
        }else if(i == 6){
            if(Input.GetKeyDown("space")){
                board3.SetActive(false);
                board4.SetActive(true);
                i++;
            }
        }else if(i == 7){
            if(Input.GetKeyDown("space")){
                board4.SetActive(false);
                board5.SetActive(true);
                i++;
            }
        }else if (i == 8){
            if(Input.GetKeyDown("space")){
                board5.SetActive(false);
                Dialog.PrintDialog("Room1");
                i++;
            }
        }
    }


}