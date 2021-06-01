using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallRoom : MonoBehaviour
{
    public static GameObject QMark;
    public static GameObject Glass;
    // public static GameObject CatchUI;
    public static bool isRoomStart = false;
    bool isDiaActive = false;
	private GameObject Hint;

	void Awake() {
		QMark = GameObject.Find("GirlQMark");
		Glass = GameObject.Find("Glass");
		Hint = GameObject.Find("Hint");
		// CatchUI = GameObject.Find("Fail");
	}

	void Start() {
		Hint.SetActive(false);
		Glass.SetActive(false);
		// CatchUI.SetActive(false);
		if (GameManager.instance.islv2FallGlassEnd) {
            isRoomStart = true;
			QMark.SetActive(true);
        }
		else {
			QMark.SetActive(false);
		}
	}

	void Update(){
		// if (CatchUI.activeSelf && Hint.activeSelf) {
		// 	if (Input.GetKeyDown("space")) {
		// 		LevelLoader.instance.LoadLevel("Level3OpenWindow");
		// 	}
		// }
	}

	void OnTriggerStay2D(Collider2D collision) {
		if(isRoomStart && collision.tag == "Player" && !isDiaActive) {
			Hint.SetActive(true);
			if (Input.GetKeyDown("space")) {
				Hint.SetActive(false);
	        	QMark.SetActive(false);
	        	Dialog.PrintDialog("Lv2FallRoom");
	        	GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
	        	Glass.SetActive(true);
	        	isDiaActive = true;
				//StartCoroutine(CheckDialogueDone());
				//GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
				//GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BedlWithNews");
		    }
	    }
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player" && Hint.activeSelf)
		{
			Hint.SetActive(false);
		}
	}

	IEnumerator CheckDialogueDone()
    {
    yield return new WaitWhile(GameManager.instance.IsDialogShow);
		LevelLoader.instance.LoadLevel("Level3MemoryTL");
		// CatchUI.SetActive(true);
		// Invoke("EndHint", 0.7f);
	}
	void EndHint(){
		Hint.SetActive(true);
	}
}
