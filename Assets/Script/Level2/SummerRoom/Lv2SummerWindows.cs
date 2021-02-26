using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lv2SummerWindows : MonoBehaviour
{
    public static GameObject LeaveHint;
    string SceneName;
    
    void Start()
    {
        LeaveHint = GameObject.Find("LeaveHint");
        LeaveHint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LeaveHint.activeSelf && Input.GetKeyDown("space")) {
            if (!GameManager.instance.islv2SummerNewsEnd) {
                SceneName = "Level2Summer"; // "Level2Winter"
                Debug.Log("transroom Level2Winter");
            } else {
				SceneName = "Level2SummerRoomWin";
			}
            GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsFlyingOut", true);
            Debug.Log("FlyingOut");
            GameManager.instance.stopMoving = true;
            StartCoroutine(waitFlyAnimOver(SceneName));
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (!GameManager.instance.islv2SummerNewsEnd) {
     	    if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
		        LeaveHint.SetActive(true);
          }
		}
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveHint.SetActive(false);
    }

    IEnumerator waitFlyAnimOver(string sceneName)
    {
        // while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        // {
        //     yield return null;
        // }
        // GameManager.instance.stopMoving = false;
        // LevelLoader.instance.LoadLevel(sceneName);
        yield return new WaitWhile(() => GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //GameManager.instance.stopMoving = false;
        LevelLoader.instance.LoadLevel(sceneName);

    }
}
