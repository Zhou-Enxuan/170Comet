using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChange : MonoBehaviour
{

    public SpriteRenderer Bubble;

    private void Awake(){
        Bubble.enabled = false;
        
    }



    private void OnTriggerStay2D(Collider2D collider){
        Debug.Log("Trigger");
        Bubble = transform.Find("bubble_pen_paper").GetComponent<SpriteRenderer>();
        Bubble.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collider){
        Debug.Log("Trigger");
        Bubble = transform.Find("bubble_pen_paper").GetComponent<SpriteRenderer>();
        Bubble.enabled = false;
    }

}
