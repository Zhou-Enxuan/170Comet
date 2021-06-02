using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2FallWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.CompareTo("Player") == 0) {
			LevelLoader.instance.LoadLevel("Level2FallWaitingRoom");
		}
	}
}
