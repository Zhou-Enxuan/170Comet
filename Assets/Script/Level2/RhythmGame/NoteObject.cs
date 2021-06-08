using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    [SerializeField] BeatScroller BeatScroller;
    private bool CanBePressed;
    [SerializeField] private GameObject End;
    [SerializeField] private GameObject StartPoint;
    private SpriteRenderer SR;
    [SerializeField] AudioSource NoteSound;
    private bool PassEnd;
    private bool IsHitted;

    void Awake()
    {
        // End = GameObject.Find("End");
        // StartPoint = GameObject.Find("Start");
        SR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SR.enabled = true;
        CanBePressed = false;
        PassEnd = false;
        IsHitted = false;
    }

    void Update()
    {
        //限制音符出现在背景框内
        if ((transform.position.x < StartPoint.transform.position.x) && (transform.position.x > End.transform.position.x) && !IsHitted) {
            SR.enabled = true;
        }else{
            SR.enabled = false;
        }

        //空格得分
        if (Input.GetKeyDown("space"))
        {
            if (!IsHitted && CanBePressed)
            {
                SR.enabled = false;
                NoteSound.Play();
                BeatScroller.score += 1;
                IsHitted = true;
            }
        }
    
        //计算得分
        if (!PassEnd && transform.position.x < End.transform.position.x)
        {
            BeatScroller.total += 1;
            PassEnd = true;
            Debug.Log("total = " + BeatScroller.total);
            Debug.Log("score = " + BeatScroller.score);
            IsHitted = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "MusicButton")
        {
            CanBePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "MusicButton")
        {
            CanBePressed = false;
        }
    }
}
