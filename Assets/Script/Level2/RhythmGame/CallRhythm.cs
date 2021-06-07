using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRhythm : MonoBehaviour
{
    [SerializeField] GameObject Rhythm;
    [SerializeField] BeatScroller BeatScroller;
    [SerializeField] BirdOutDoorMovement birdOutDoorMovement;
    [SerializeField] bool IsInFace = false;
    public bool IsGameEnded;
    private GameObject SpaceHint;
    private GameObject RhythmHint;
    private GameObject Fail;
    private GameObject Hint;

    void Awake() 
    {
        Rhythm = GameObject.Find("RhythmGame");
        SpaceHint = GameObject.Find("SpaceHint");
        RhythmHint = GameObject.Find("RhythmHint");
        Fail = GameObject.Find("Fail");
        Hint = GameObject.Find("Hint");
    }

    void Start()
    {
        Rhythm.SetActive(false);
        IsGameEnded = false;
        RhythmHint.SetActive(false);
        SpaceHint.SetActive(false);
        Fail.SetActive(false);
        Hint.SetActive(false);
    }

    void Update()
    {
        if (Rhythm.activeSelf)
        {
            GameManager.instance.stopMoving = true;
        }
        if (IsInFace)
        {
            if (Input.GetKeyDown("space"))
            {
                GameManager.instance.stopMoving = true;
                SpaceHint.SetActive(false);
                
                Rhythm.SetActive(true);
                Rhythm.transform.position = new Vector3(GameObject.Find("Main Camera").transform.position.x, transform.position.y, 0f);
                RhythmHint.SetActive(true);
                BeatScroller.score = 0;
                BeatScroller.total = 0;
                IsInFace = false;
            }
        }

        if (BeatScroller.Reset)
        if (BeatScroller.total == 5)
        {
            Rhythm.SetActive(false);
            RhythmHint.SetActive(false);
            if (BeatScroller.score >= 4)
            {
                IsGameEnded = true;
            }
            else
            {
                Fail.SetActive(true);
                StartCoroutine(WaitanimDone());
                GamePlaySystemManager.isRhythmFailed = true;
            }
            
        }else{
            Hint.SetActive(false);
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitWhile(() => Fail.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        
        if (Input.GetKeyDown("space"))
        {
            // Fail.SetActive(false);
            // Hint.SetActive(false);
            // GameManager.instance.stopMoving = false;
            // BeatScroller.Reset = false;
            // BeatScroller.score = 0;
            // BeatScroller.total = 0;
            // RhythmReset.ResetNote = true;
            // RhythmReset.ResetButton = true;
            LevelLoader.instance.LoadLevel("Level2WinRhythm");
        }else{
            Hint.SetActive(true);
        }
    }

    //当bird撞上sadFace时触发五线谱
    void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag.CompareTo("Player") == 0 && !Rhythm.activeSelf) {
            IsInFace = true;
            SpaceHint.SetActive(true);
    	}
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag.CompareTo("Player") == 0){
            IsInFace = false;
            SpaceHint.SetActive(false);
        }
    }
}
