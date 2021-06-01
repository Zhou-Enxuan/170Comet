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
            if (!GameManager.instance.islv2SummerNewsEnd) {
                SceneName = "Level2Summer";
            }
            else {
                SceneName = "Level2Fall";
            }
            GameObject.Find("Player").GetComponent<BirdInDoorMovement>().Numdirection = 0;
            GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            GameManager.instance.stopMoving = true;
            StartCoroutine(waitFlyAnimOver(SceneName));
            LeaveTip.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (GameObject.Find("GirlQMark") == null) {
     	    if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
		        LeaveTip.SetActive(true);
            }
		}
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveTip.SetActive(false);
    }

    IEnumerator waitFlyAnimOver(string sceneName) {
        
        SoundManager.playSEOne("birdFlyOut", 0.7f);
        while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        GameManager.instance.stopMoving = false;
        LevelLoader.instance.LoadLevel(sceneName);

    }

}
