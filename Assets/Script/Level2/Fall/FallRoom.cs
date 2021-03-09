using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRoom : MonoBehaviour
{
    public static GameObject QMark;
    public static bool isRoomStart = false;
    bool isDiaActive = false;

	void Awake() {
		QMark = GameObject.Find("GirlQMark");
		if (GameManager.instance.islv2FallGlassEnd) {
            isRoomStart = true;
			QMark.SetActive(true);
        }
		else {
			QMark.SetActive(false);
		}
	}

	void Update(){

	}

	void OnTriggerStay2D(Collider2D collision) {
		if(isRoomStart && collision.tag == "Player" && !isDiaActive) {
			if (Input.GetKeyDown("space")) {
	        	QMark.SetActive(false);
	        	Dialog.PrintDialog("Lv2FallRoom");
	        	isDiaActive = true;
				//GameObject.Find("Player").GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
				//GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BedlWithNews");
		    }
	    }
	}
}
