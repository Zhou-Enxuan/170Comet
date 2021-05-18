using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoliderTimeline2 : MonoBehaviour
{ 
	public GameObject soldier01;
    public GameObject soldier02;
    public GameObject soldier03;
    public GameObject failUI;
    public GameObject Hint;
    public static GameObject player;
    public static GameObject girlTimeLine;

    void Awake() {
        player = GameObject.Find("PlayerGirl");
        girlTimeLine = GameObject.Find("GirlTimeline");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        TimelineGameManager.isTimeline = false;
        TimelineGameManager.GetDirector(girlTimeLine.GetComponent<PlayableDirector>());
        girlTimeLine.SetActive(false);
        soldier01.SetActive(false);
        soldier02.SetActive(false);
        soldier03.SetActive(false);
    	failUI.SetActive(false);     
    	Hint.SetActive(false); 

    }

    // Update is called once per frame
    void Update()
    {
        if(girlTimeLine.activeSelf && girlTimeLine.GetComponent<PlayableDirector>().enabled == true) {
    		GameManager.instance.stopMoving = true;
	        player.GetComponent<Animator>().SetFloat("Speed", 0.0f);
    		player.transform.position = new Vector2(-10.56f, player.transform.position.y);
        	soldier01.SetActive(true);
        	soldier02.SetActive(true);
        	soldier03.SetActive(true);
        } 
        else if (girlTimeLine.GetComponent<PlayableDirector>().enabled == false){
        		player.transform.position = new Vector2(-11.09f, -1.35f);
        }

        if (GirlTimeLineMovement.isPlayFailUI) {
	        failUI.SetActive(true);     
       		Invoke("EndHint",0.7f);
        }

        if (Hint.activeSelf && Input.GetKeyDown(KeyCode.Space)) {
        	LevelLoader.instance.LoadLevel("Level4");
        }
    }

    public void animIsWalking() {
    	soldier01.GetComponent<Animator>().SetBool("isWalking", true);
    	soldier02.GetComponent<Animator>().SetBool("isWalking", true);
    	soldier03.GetComponent<Animator>().SetBool("isWalking", true);
    }

    public void animIsntWalking() {
    	soldier01.GetComponent<Animator>().SetBool("isWalking", false);
    	soldier02.GetComponent<Animator>().SetBool("isWalking", false);
    	soldier03.GetComponent<Animator>().SetBool("isWalking", false);
    }

    public void animFaceA() {
    	soldier03.GetComponent<Animator>().SetBool("FaceR", false);
    }

    void EndHint() {
        Hint.SetActive(true);
    }
}
