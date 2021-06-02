using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2R1Window : MonoBehaviour
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
            if (!GameManager.instance.isLv2Npc) {
                SceneName = "Level2Winter"; // "Level2Winter"
                Debug.Log("transroom Level2Winter");
            } 
            else if (!GameManager.instance.isLv2WinterEnd) {
                SceneName = "Level2WinRhythm"; // "Level2WinRhythm"
                Debug.Log("transroom Level2WinRhythm");
            }
            else if (!GameManager.instance.isLv2Flower){
                SceneName = "Level2WinFlower"; // "Level2WinFlower"
                Debug.Log("transroom Level2WinFlower");
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
        if (!GameManager.instance.isLv2Flower) {
     	    if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
		        LeaveTip.SetActive(true);
            }
		}
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveTip.SetActive(false);
    }

    IEnumerator waitFlyAnimOver(string sceneName)
    {
        SoundManager.playSEOne("birdFlyOut", 0.7f);
        while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        LevelLoader.instance.LoadLevel(sceneName);
        GameManager.instance.stopMoving = false;

    }
}
