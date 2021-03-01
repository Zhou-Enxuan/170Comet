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

	void Awake() {
		QMark = GameObject.Find("GirlQMark");
		if (GameManager.instance.islv2SummerNewsEnd) {
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
	        	Dialog.PrintDialog("Lv2NewsRoom");
		    }
	    }
	}
}
