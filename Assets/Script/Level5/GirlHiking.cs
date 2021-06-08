using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlHiking : MonoBehaviour
{
    public event Action OnHiking;

    private Rigidbody2D rb;
    private GameObject Bird;
    [SerializeField] float Speed;
    [SerializeField] private Transform StartPoint, Dia1, Dia2, Dia3, Dia4;
    [SerializeField] private Animator SkyAnim, MountainAnim;
    private float start, point1, point2, point3, point4;
    private Animator Anim;
    private bool IsMoving;
    private int order;
    private GameObject Rhythm1, Rhythm2, Rhythm3, Rhythm4;
    private GameObject Fail;
    private GameObject Continue;
    [SerializeField] AudioSource FailSound;
    private GameObject KeyHint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        start = StartPoint.position.x;
        point1 = Dia1.position.x;
        point2 = Dia2.position.x;
        point3 = Dia3.position.x;
        point4 = Dia4.position.x;
        Anim = GetComponent<Animator>();
        IsMoving = true;
        Bird = GameObject.Find("Player2");
        Rhythm1 = GameObject.Find("Rhythm1");
        Rhythm2 = GameObject.Find("Rhythm2");
        Rhythm3 = GameObject.Find("Rhythm3");
        Rhythm4 = GameObject.Find("Rhythm4");
        Fail = GameObject.Find("Fail");
        Continue = GameObject.Find("Continue");//临时
        KeyHint = GameObject.Find("KeyHint");
    }

    void Start()
    {
        SoundManager.playBgm(8);
        OnHiking += Hiking;
        order = 0;
        Rhythm1.SetActive(false);
        Rhythm2.SetActive(false);
        Rhythm3.SetActive(false);
        Rhythm4.SetActive(false);
        Continue.SetActive(false);//临时
        KeyHint.SetActive(false);
    }

    void Update()
    {
        OnHiking?.Invoke();
    }

    void LateUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y + 2f;
        
        // GameObject.Find("RhythmGames").transform.position = new Vector3(x, y, 0f);
        
    }

    private void Hiking()
    {
        if(IsMoving)
        {
            Bird.transform.position = transform.position + new Vector3(-1.14f, 0.5f, 0f);
            if (Fail.activeSelf)
            {
                //sky停止变色
                SkyAnim.enabled = false;
                MountainAnim.enabled = false;
                //女孩静止
                rb.velocity = Vector2.zero;
                IsMoving = false;
                GirlStop();
                FailSound.Play();
            }
            else
            {
                if (transform.position.x > start || transform.position.x > point1 || transform.position.x > point2 || transform.position.x > point3 || transform.position.x > point4)
                {
                    //sky停止变色
                    SkyAnim.enabled = false;
                    MountainAnim.enabled = false;
                    //女孩静止
                    rb.velocity = Vector2.zero;
                    IsMoving = false;
                    if (!Fail.activeSelf)
                        GirlStop();
                }
                else
                {
                    rb.velocity = new Vector2(Speed, 0);
                    //sky变色
                    SkyAnim.enabled = true;
                    MountainAnim.enabled = true;
                }
            }
        }
        
    }

    private void GirlStop()
    {
        if (transform.position.x <= start)
        {
            Anim.SetTrigger("Stop");
        }else{
            Anim.SetTrigger("Ready");
        }
        
        if (transform.position.x > start)
        {
            start = 50f;
            Dialog.PrintDialog("Lv5Start");
            order = 0;
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point1)
        {
            point1 = 50f;
            Dialog.PrintDialog("Lv5Part1");
            order = 1;
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point2)
        {
            point2 = 50f;
            Dialog.PrintDialog("Lv5Part2");
            order = 2;
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point3)
        {
            point3 = 50f;
            Dialog.PrintDialog("Lv5Part3");
            order = 3;
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point4)
        {
            point4 = 50f;
            Dialog.PrintDialog("Lv5Part4");
            StartCoroutine(CheckSceneDone());
        }
    }

    IEnumerator CheckDialogueDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Anim.SetTrigger("Move");
        IsMoving = true;
        if (order == 0){
            Rhythm1.SetActive(true);
            KeyHint.SetActive(true);
        }
        else if (order == 1){
            Rhythm2.SetActive(true);
        }
        else if (order == 2){
            Rhythm3.SetActive(true);
        }
        else if (order == 3){
            Rhythm4.SetActive(true);
        }
    }

    IEnumerator CheckSceneDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Debug.Log("The End");
        Anim.enabled = false;
        LevelLoader.instance.LoadLevel("Ending");
        GameManager.instance.stopMoving = true;//临时
    }
}

