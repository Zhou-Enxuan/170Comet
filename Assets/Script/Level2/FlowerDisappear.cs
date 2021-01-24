using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDisappear : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) {
        Debug.Log("pengzhuang");

    	if (other.tag.CompareTo("Player") == 0 && Input.GetKeyDown("space")) {
            Debug.Log("jianhua");
            GameObject.Find("Flower").SetActive(false);
    	}
    }
}
