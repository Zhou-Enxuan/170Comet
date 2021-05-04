using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FallGlassWindow : MonoBehaviour
{
	bool isActive = false;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive) {
        	LevelLoader.instance.LoadLevel("Level2FallGlass");
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //if (!GameManager.instance.isLv2Flower) {
     	    //if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
     	    if (other.tag.CompareTo("Player") == 0) {
				isActive = true;
            }
		//}
	}
}
