using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2SWRoomWindow : MonoBehaviour
{
    public static GameObject LeaveTip;
    string SceneName;
    
    void Start()
    {
        LeaveTip = GameObject.Find("LeaveTip");
        LeaveTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LeaveTip.activeSelf && Input.GetKeyDown("space")) {
        	GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            GameManager.instance.stopMoving = true;
            StartCoroutine(waitFlyAnimOver("Level2Fall"));
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        //if (!GameManager.instance.isLv2Flower) {
     	    if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
		        LeaveTip.SetActive(true);
            }
		//}
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveTip.SetActive(false);
    }

    IEnumerator waitFlyAnimOver(string sceneName) {
        while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        LevelLoader.instance.LoadLevel(sceneName);

    }

}
