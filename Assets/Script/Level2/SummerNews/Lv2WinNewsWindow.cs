﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lv2WinNewsWindow : MonoBehaviour
{
	// public GameObject LeaveTip;
	// public bool isLevel1End; // => GameObject.FindGameObjectWithTag("Player").GetComponent<PaletteController>().isLevel1End == true;
	// public bool isDialogShown;// => FindObjectOfType<Dialog>() == null;
	//public bool isLTShown = false;

	void Start() {
		// if (SceneManager.GetActiveScene().name == "Level1") {
		// 	LeaveTip = GameObject.Find("LeaveTip");
		// 	LeaveTip.SetActive(false);
		// }
	}

	// void OnTriggerExit2D(Collider2D other) {
 //    	if (SceneManager.GetActiveScene().name == "Level1" && other.tag.CompareTo("Player") == 0 && GameObject.Find("DialogBox") == null) {
	// 		LeaveTip.SetActive(false);
	// 	}
	// }

    void OnTriggerEnter2D(Collider2D other) {
  //   	if (SceneManager.GetActiveScene().name == "Level1" && other.tag.CompareTo("Player") == 0 && GameObject.Find("DialogBox") == null) {
		// 	LeaveTip.SetActive(true);
		// }
		if (other.tag.CompareTo("Player") == 0) {
			if (GameManager.instance.islv2SummerNewsEnd) {
				LevelLoader.instance.LoadLevel("Level2SumWinRoom");
			}else {
				LevelLoader.instance.LoadLevel("Level2WaitingRoom");
			}
			
		}
	}
}
