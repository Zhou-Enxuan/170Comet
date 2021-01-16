using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

	public static string NextSceneName;
	public bool isSceneChanged = false; //是否完成场景切换
	Vector3 PlayerPosition; //玩家位置
	// void Awake() {
	// 	// if (SceneManager.GetActiveScene().name == "Level1") {
	// 	// 	LeaveTip = GameObject.Find("LeaveTip");
	// 	// 	LeaveTip.SetActive(false);
	// 	// }
	// }
	
	void Update(){
			if (SceneManager.GetActiveScene().name == "Loading" && !isSceneChanged) {
				Invoke( "LoadScene" , 1f);
				GameObject.Find("Player").GetComponent<Transform>().localPosition = PlayerPosition;
				isSceneChanged = true;
				Debug.Log(NextSceneName + "active");
			} //else {
				if (SceneManager.GetActiveScene().name == "Initial") {
					isSceneChanged = false;
					ActiveScene("Loading");
					NextSceneName = "Level1";
				}
				if (SceneManager.GetActiveScene().name == "Level1") {
					PlayerPosition  = new Vector3(-3.5f, -3f, 0f);
					isSceneChanged = false;
					if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space")) {
						NextSceneName = "Level2";
						ActiveScene("Loading");
					}
				}
				if (SceneManager.GetActiveScene().name == "Level2") {
					PlayerPosition  = new Vector3(0f,0f,0f);
					isSceneChanged = false;
					if (GameObject.Find("Window") == null) {
						ActiveScene("Loading");
						NextSceneName = "Level1";
					}
				}
			//}
			
	}

	void LoadScene() {
		ActiveScene(NextSceneName);
	}

	public void ActiveScene(string ToSceneName){
		SceneManager.LoadScene(ToSceneName);
	}

 //    void OnTriggerStay2D(Collider2D other) {
	// 	if (SceneManager.GetActiveScene().name == "Level1" && other.tag.CompareTo("Player") == 0 && Level1End && DialogShown) {
	// 		LeaveTip.SetActive(true);
	// 		if (Input.GetKeyDown("space")) {
	// 			SceneManager.LoadScene("Level2");
	// 		}
	// 	}
	// 	if (SceneManager.GetActiveScene().name == "Level1.1" && other.tag.CompareTo("Player") == 0) {
	// 		LeaveTip.SetActive(true);
	// 		if (Input.GetKeyDown("space")) {
	// 			SceneManager.LoadScene("Level2");
	// 		}
	// 	}
	// }

}

