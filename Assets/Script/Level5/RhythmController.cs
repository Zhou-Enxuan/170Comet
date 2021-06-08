using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    private GameObject Rhythm1, Rhythm2, Rhythm3, Rhythm4;
    [SerializeField] BeatScroller BeatScroller1, BeatScroller2, BeatScroller3, BeatScroller4;
    private GameObject Fail;
    private GameObject Hint;
    public bool IsFailed;
    private GameObject KeyHint;

    void Awake()
    {
        Rhythm1 = GameObject.Find("Rhythm1");
        Rhythm2 = GameObject.Find("Rhythm2");
        Rhythm3 = GameObject.Find("Rhythm3");
        Rhythm4 = GameObject.Find("Rhythm4");
        Fail = GameObject.Find("Fail");
        Hint = GameObject.Find("Hint");
        KeyHint = GameObject.Find("KeyHint");
    }

    void Start()
    {
        //Rhythm1.SetActive(true);
        Fail.SetActive(false);
        Hint.SetActive(false);
        IsFailed = false;
        // KeyHint.SetActive(false);
    }

    void Update()
    {
        if (KeyHint.activeSelf && Input.anyKeyDown)
        {
            KeyHint.SetActive(false);
        }
        if (BeatScroller1.total == 5)
        {
            Rhythm1.SetActive(false);
            if (BeatScroller1.score >= 4)
            {
                // IsGameEnded = true;
            }
            else
            {
                IsFailed = true;
                Fail.SetActive(true);
                StartCoroutine(WaitanimDone());
            }
        }

        if (BeatScroller2.total == 5)
        {
            Rhythm2.SetActive(false);
            if (BeatScroller2.score >= 4)
            {
                // IsGameEnded = true;
            }
            else
            {
                IsFailed = true;
                Fail.SetActive(true);
                StartCoroutine(WaitanimDone());
            }
        }

        if (BeatScroller3.total == 5)
        {
            Rhythm3.SetActive(false);
            if (BeatScroller3.score >= 4)
            {
                // IsGameEnded = true;
            }
            else
            {
                IsFailed = true;
                Fail.SetActive(true);
                StartCoroutine(WaitanimDone());
            }
        }

        if (BeatScroller4.total == 5)
        {
            Rhythm4.SetActive(false);
            if (BeatScroller4.score >= 4)
            {
                // IsGameEnded = true;
            }
            else
            {
                IsFailed = true;
                Fail.SetActive(true);
                StartCoroutine(WaitanimDone());
            }
        }

        if (IsFailed)
        {
            Rhythm1.SetActive(false);
            Rhythm2.SetActive(false);
            Rhythm3.SetActive(false);
            Rhythm4.SetActive(false);
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitWhile(() => Fail.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        
        if (Input.GetKeyDown("space"))
        {
            // Fail.SetActive(false);
            // Hint.SetActive(false);
            LevelLoader.instance.LoadLevel("Level5");
        }else{
            Hint.SetActive(true);
        }
    }
}
