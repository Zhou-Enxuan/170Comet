using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level4Timeline : MonoBehaviour
{
    public static GameObject girlWatchTL;
	public Camera mainCamera;
	public GameObject hat;

	void Awake() {
		girlWatchTL = GameObject.Find("GirlWatchTimeline");
	}
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.playBgm(5);
        // //this.GetComponent<SoldierMovement>().enabled = false;
        this.GetComponent<Animator>().SetBool("isTimeline", true);
        TimelineGameManager.isTimeline = false;
        TimelineGameManager.GetDirector(girlWatchTL.GetComponent<PlayableDirector>());
        girlWatchTL.SetActive(false);
        hat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelineGameManager.isTimeline) {
        	mainCamera.depth = -1;
        }
        else if (girlWatchTL.GetComponent<PlayableDirector>().enabled == false) { // && !this.GetComponent<SoldierMovement>().enabled) {
        	mainCamera.depth = 1;
        	LevelLoader.instance.LoadLevel("Level4");
        	// GameManager.instance.stopMoving = false;
        	// this.GetComponent<SoldierMovement>().enabled = true;
        	this.GetComponent<Animator>().SetBool("isTimeline", false);
        }
    }
    
    public void hatActive() {
    	hat.SetActive(true);
    }
}
