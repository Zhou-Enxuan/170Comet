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

	void Awake() {
		Hint = GameObject.Find("Hint");
		QMark = GameObject.Find("GirlQMark");
		if (GameManager.instance.islv2SummerNewsEnd) {
            isRoomStart = true;
			QMark.SetActive(true);
        }
		else {
			QMark.SetActive(false);
		}
	}

	void Start() {
		Hint.SetActive(false);
	}

	void Update(){

	}

	void OnTriggerStay2D(Collider2D collision) {
		if(isRoomStart && collision.tag == "Player" && !isDiaActive) {
			Hint.SetActive(true);
			if (Input.GetKeyDown("space")) {
				Hint.SetActive(false);
	        	QMark.SetActive(false);
	        	Dialog.PrintDialog("Lv2NewsRoom");
	        	isDiaActive = true;
				GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BedlWithNews");
		    }
	    }
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player" && Hint.activeSelf) {
			Hint.SetActive(false);
		}
	}
}
