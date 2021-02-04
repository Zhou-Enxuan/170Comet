using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDisappear : MonoBehaviour
{
	public static bool isPickFlower;

    void OnTriggerStay2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0 && Input.GetKeyDown("space")) {
    		GameObject.Find("NpcTwo").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Flower").SetActive(false);
            isPickFlower = true;
    	}
    }
}
