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
            SceneName = "Level2Summer"; 
            //Debug.Log("transroom Level2Winter");
            GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            GameManager.instance.stopMoving = true;
            StartCoroutine(waitFlyAnimOver(SceneName));
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
 	    if (other.tag.CompareTo("Player") == 0 && !GameManager.instance.IsDialogShow()) {
	        LeaveHint.SetActive(true);
      }
	}

    void OnTriggerExit2D(Collider2D collision) {
        LeaveHint.SetActive(false);
    }

    IEnumerator waitFlyAnimOver(string sceneName)
    {
        while (GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        GameManager.instance.stopMoving = false;
        LevelLoader.instance.LoadLevel(sceneName);

    }
}
