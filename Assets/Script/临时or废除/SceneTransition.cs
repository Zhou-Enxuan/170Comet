using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

	public static string NextSceneName;
	public bool isSceneChanged = false; //是否完成场景切换
	// Vector3 PlayerPosition; //玩家位置
	// void Awake() {
	// 	// if (SceneManager.GetActiveScene().name == "Level1") {
	// 	// 	LeaveTip = GameObject.Find("LeaveTip");
	// 	// 	LeaveTip.SetActive(false);
	// 	// }
	// }
	
	void Update(){
			if (SceneManager.GetActiveScene().name == "Loading" && !isSceneChanged) {
				Invoke( "LoadingScene" , 1f);
				//GameObject.Find("Player").GetComponent<Transform>().localPosition = PlayerPosition;
				isSceneChanged = true;
				Debug.Log(NextSceneName + "active");
			} 
			//else {445
			if (SceneManager.GetActiveScene().name == "Initial") {
				isSceneChanged = false;
				ActiveScene("Loading");
				NextSceneName = "Level1";
			}
			if (SceneManager.GetActiveScene().name == "Level1") {
				//GameObject.Find("Player").GetComponent<PlayerAnimation1>().Level2DefultAnimation();
				isSceneChanged = false;
				TipsLeave("Level2Winter");
				// if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space")) {
				// 	//GameObject.Find("Player").GetComponent<InDoorAnimation>().FlyOutAnimation();
				// 	//StartCoroutine(WaitCoroutine());
				// 	NextSceneName = "Level2Winter";
				// 	ActiveScene("Loading");
				// }
			}
			if (SceneManager.GetActiveScene().name == "Level2Winter" || SceneManager.GetActiveScene().name == "Level2WinFlower") {
				// PlayerPosition  = new Vector2(1.7f, 0.7f);
				//GameObject.Find("Player").GetComponent<PlayerAnimation1>().Level1DefultAnimation();
				isSceneChanged = false;
				if (GameObject.Find("Window") == null) {
					ActiveScene("Loading");
					NextSceneName = "Level2Room1";
				}
			}
			if(SceneManager.GetActiveScene().name == "Level2Room1") {
				isSceneChanged = false;
				if (!GirlQuestion.isRoomStart) {
					TipsLeave("Level2Winter");
					Debug.Log("Level2Winter");
				}
				else {
					TipsLeave("Level2WinFlower");
					Debug.Log("Level2WinFlower");
				}
			}
			// 	if (SceneManager.GetActiveScene().name == "Level1") {
			// 		PlayerPosition  = new Vector2(-6.1f, -1.9f);
			// 		//GameObject.Find("Player").GetComponent<PlayerAnimation1>().Level2DefultAnimation();
			// 		isSceneChanged = false;
			// 		if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space")) {
						
			// 			GameObject.Find("Player").GetComponent<PlayerAnimation1>().FlyOutAnimation();
			// 			StartCoroutine(WaitCoroutine());
			// 			NextSceneName = "Level2";
			// 			ActiveScene("Loading");
			// 		}
			// 		if(GamePlaySystemManager.isLevel1Mission2End && Input.GetKeyDown("space") && GameObject.Find("DialogBox") == null) {
			// 			NextSceneName = "Level2SummerRoom";
			// 			ActiveScene("Loading");
			// 		}
			// 	}
			// 	if (SceneManager.GetActiveScene().name == "Level2") {
			// 		PlayerPosition  = new Vector2(1.7f, 0.7f);
			// 		//GameObject.Find("Player").GetComponent<PlayerAnimation1>().Level1DefultAnimation();
			// 		isSceneChanged = false;
			// 		if (GameObject.Find("Window") == null) {
			// 			if(!GamePlaySystemManager.isLevel1Mission2End){
			// 				ActiveScene("Loading");
			// 				NextSceneName = "Level1";
			// 			}
			// 			else {
			// 				ActiveScene("Loading");
			// 				NextSceneName = "Level2SummerRoom";
			// 			}
			// 		}
			// 	}
			// 	if (SceneManager.GetActiveScene().name == "Level2SummerRoom") {
			// 		PlayerPosition  = new Vector2(-3.6f, -0.5f);
			// 		//GameObject.FindGameObjectWithTag("Player").SetActive(false);
			// 		isSceneChanged = false;
			// 		if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space")) {
			// 			NextSceneName = "Level2"; //summer之后改为新建的VilligeScene
			// 			ActiveScene("Loading");
			// 		}
			// 	}

			//}
			
	}

	void LoadingScene() {
		ActiveScene(NextSceneName);
	}

	public void ActiveScene(string ToSceneName){
		SceneManager.LoadScene(ToSceneName);
	}

	public void TipsLeave(string SceneName) {
		if (GameObject.Find("LeaveTip") != null && Input.GetKeyDown("space")) {
			ActiveScene("Loading");
			NextSceneName = SceneName;
			Debug.Log("SceneName");
		}
	}

	IEnumerator WaitCoroutine(){
		yield return new WaitForSeconds(5);
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

