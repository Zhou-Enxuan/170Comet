using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDisappear : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0 && Input.GetKeyDown("space") && !GameManager.instance.IsDialogShow()) {
    		GameObject.Find("NpcTwo").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Flower").SetActive(false);
            GameManager.instance.GetFlower();
    	}
    }
}
