using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDisappear : MonoBehaviour
{
	public static bool isPickFlower = false;

    void OnTriggerStay2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0 && Input.GetKeyDown("space")) {
    		GameObject.Find("NpcTwo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/BrownManTurn/A_Npc02Turn04");
            GameObject.Find("Flower").SetActive(false);
            isPickFlower = true;
    	}
    }
}
