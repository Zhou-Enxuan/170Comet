using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2R1Window : MonoBehaviour
{
    public static GameObject LeaveTip;
    void Start()
    {
        LeaveTip = GameObject.Find("LeaveTip");
        LeaveTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
     	if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
			LeaveTip.SetActive(true);
		}
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveTip.SetActive(false);
    }
}
