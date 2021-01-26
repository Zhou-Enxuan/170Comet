using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScore : MonoBehaviour
{
	//score setting
    public static int currentScore = 0;
    private static int scorePerNote = 100;
    public static int totalScore = 0;
    public int totalS = 0;
    public bool IsGameEnded = false;

    public static GameObject RhythmGame;
    public static GameObject SadFace;

    void Start() {
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        SadFace = GameObject.FindGameObjectWithTag("SadFace");
    }

    void Update() {
    	totalS = totalScore;
        //过关 - 得到500分
        
	    if (currentScore == 200 && totalScore == 500) {
	        //stop rhythm game & 五线谱消失
	        RhythmGame.SetActive(false); 
	        //continue controlling the bird
	        GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = false;
	        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            IsGameEnded = true;

	        //SadFace.SetActive(true);
	        //SadFace.GetComponent<CallStaff>().enabled = true;
	    }
	    //没过关 - 未得到500分
	    else if (currentScore < 200 && totalScore == 500) {
	        //stop rhythm game & 五线谱消失
	        RhythmGame.SetActive(false);
	        //continue controlling the bird
	        GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = false;
	        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
	        SadFace.SetActive(true);
            //SadFace.GetComponent<CallStaff>().enabled = true;
            //Debug.Log("!!!!!!");
	    }
        
        
    }

    public static void NoteHit() {
        currentScore += scorePerNote;
        //totalScore += scorePerNote;
    }

    public static void NotePass() {
        totalScore += scorePerNote;
    }
}
