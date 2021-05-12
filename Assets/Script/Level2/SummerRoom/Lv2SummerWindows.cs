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
            SceneName = "Level2SummerTimeline";
            //Debug.Log("transroom Level2Winter");
            GameObject.Find("Player").GetComponent<BirdInDoorMovement>().Numdirection = 0;
            GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("Player").GetComponent<Animator>().enabled = true;
            //GameObject.Find("Player").GetComponent<Animator>().SetTrigger("FlyOut");
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsFlyingOut", true);
            //Debug.Log("FlyingOut");
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
        yield return new WaitForSeconds(1.5f);
        //yield return new WaitWhile(() => GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //GameManager.instance.stopMoving = false;
        LevelLoader.instance.LoadLevel(sceneName);

    }
}
