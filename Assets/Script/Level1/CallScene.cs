using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CallScene : MonoBehaviour
{
	public GameObject LeaveTip;
	GamePlaySystemManager GamePlaySystemManager;
	// public bool isLevel1End; // => GameObject.FindGameObjectWithTag("Player").GetComponent<PaletteController>().isLevel1End == true;
	// public bool isDialogShown;// => FindObjectOfType<Dialog>() == null;
	//public bool isLTShown = false;

	void Start() {
		GamePlaySystemManager = GameObject.Find("GamePlaySystemManager").GetComponent<GamePlaySystemManager>();
		if (SceneManager.GetActiveScene().name == "Level1") {
			LeaveTip = GameObject.Find("LeaveTip");
			LeaveTip.SetActive(false);
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
    	if (SceneManager.GetActiveScene().name == "Level1" && other.tag.CompareTo("Player") == 0 && GamePlaySystemManager.isLevel1End && GameObject.Find("DialogBox") == null) {
			LeaveTip.SetActive(true);
		}
		if (SceneManager.GetActiveScene().name == "Level2" && other.tag.CompareTo("Player") == 0) {
			Destroy(GameObject.Find("Window"));
		}
	}
}
