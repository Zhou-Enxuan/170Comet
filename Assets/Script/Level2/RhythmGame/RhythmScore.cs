using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmScore : MonoBehaviour
{
	//score setting
    public static int currentScore;
    private static int scorePerNote = 100;
    public static int totalScore;
    public int totalS = 0;
    public bool IsGameEnded = false;
    public bool PlayAgain;

    public static GameObject RhythmGame;
    public static GameObject SadFace;

    void OnEnable() {
        RhythmGame = GameObject.FindGameObjectWithTag("RhythmGame");
        SadFace = GameObject.FindGameObjectWithTag("SadFace");
        currentScore = 0;
        totalScore = 0;
        PlayAgain = false;
    }

    void Update() {
    	totalS = totalScore;
<<<<<<< Updated upstream
        //过关 - 得到500分
        
	    if (currentScore >= 200 && totalScore >= 500) {
=======

        //过关 - 得到200分
	    if (currentScore >= 200 && totalScore == 500) {
>>>>>>> Stashed changes
	        //stop rhythm game & 五线谱消失
	        RhythmGame.SetActive(false); 
	        //continue controlling the biraad
	        GameObject.Find("Player").GetComponent<Rigidbody2D>().isKinematic = false;
	        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
            IsGameEnded = true;
<<<<<<< Updated upstream
            Debug.Log("过关");
	        //SadFace.SetActive(true);
	        //SadFace.GetComponent<CallStaff>().enabled = true;
=======
>>>>>>> Stashed changes
	    }
	    //没过关 - 未得到200分
	    else if (currentScore < 200 && totalScore == 500) {
            PlayAgain = true;
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
