using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteController : MonoBehaviour
{

	public int i;
	public GameObject board;
	public GameObject board2;
	public GameObject board3;
	public GameObject board4;
	public GameObject board5;

    private void OnTriggerStay2D(Collider2D collision){
    	if(i < 2){
			if(collision.tag == "palette" && Input.GetKeyDown("space")){
            	Destroy(collision.gameObject);
            	i++;
        	}
		}
		if(i == 2){
			if(collision.tag == "trigger" && Input.GetKeyDown("space")){
            	board.SetActive(true);
            	i ++;
        	}
		}else if(i == 3){
			if(Input.GetKeyDown("space")){
        		board.SetActive(false);
        		board2.SetActive(true);
            	i++;
        	}
		}else if(i == 4){
			if(Input.GetKeyDown("space")){
        		board2.SetActive(false);
        		board3.SetActive(true);
            	i++;
        	}
		}else if(i == 5){
			if(Input.GetKeyDown("space")){
        		board3.SetActive(false);
        		board4.SetActive(true);
        		i++;
        	}
		}else if(i == 6){
			if(Input.GetKeyDown("space")){
        		board4.SetActive(false);
        		board5.SetActive(true);
        		i++;
        	}
		}else{
			if(Input.GetKeyDown("space")){
				board5.SetActive(false);
			}
		}
    }


}
