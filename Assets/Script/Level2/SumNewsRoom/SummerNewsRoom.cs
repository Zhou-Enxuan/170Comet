//By Huazhen Xu
//完成报纸(islv2SummerNewsEnd==true)进入房间时使用
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummerNewsRoom : MonoBehaviour
{
    public static GameObject QMark;
    public static bool isRoomStart = false;
    bool isDiaActive = false;
	private GameObject Hint;
    bool isActive;

	void Awake() {
		Hint = GameObject.Find("Hint");
		QMark = GameObject.Find("GirlQMark");
	}

	void Start() {
		isActive = false;
		Hint.SetActive(false);
		if (GameManager.instance.islv2SummerNewsEnd) {
            isRoomStart = true;
			QMark.SetActive(true);
        }
		else {
			Dialog.PrintDialog("Lv2WithoutNews");
			QMark.SetActive(false);
		}
	}

	void Update(){
		if (isActive && Hint.activeSelf) {
			if (Input.GetKeyDown("space")) {
				Hint.SetActive(false);
	        	QMark.SetActive(false);
	        	Dialog.PrintDialog("Lv2NewsRoom");
	        	isDiaActive = true;
				GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BedlWithNews");
				StartCoroutine(CheckDialogDone());
		    }
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		if(isRoomStart && collision.tag == "Player" && !isDiaActive) {
			Hint.SetActive(true);
			isActive = true;
	    }
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player" && Hint.activeSelf) {
			Hint.SetActive(false);
			isActive = false;
		}
	}

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        GameManager.instance.stopMoving = false;
        LevelLoader.instance.LoadLevel("Level2Fall");

    }
}
