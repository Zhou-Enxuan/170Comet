using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GirlAction : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;
    private GameObject Hint;
    private SpriteRenderer sprite;
    public float Speed;
    private bool IsMoving;
    private GameObject Drawing;
    public bool IsCollidingSoldier;
    public Transform EndPoint;
    private float pointX;
    public bool IsArrived;

    public GameObject failUI;
    public GameObject restartHint;
    private bool isEnd;
    private bool Restart;

    private void Awake()
    {

        SoundManager.playBgm(6);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
        pointX = EndPoint.position.x;
    }

    void Start()
    {
        IsMoving = true;
        IsCollidingSoldier = false;
        Destroy(EndPoint.gameObject);
        IsArrived = false;
        failUI.SetActive(false);
        restartHint.SetActive(false);
        isEnd = false;
        Restart = false;
    }


    void Update() 
    {
        if (!IsCollidingSoldier)
        {
            GirlMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (Restart)
                FailScreen("Level4Part2");
        }
        // if (Hint.activeSelf){
        //     if (Input.GetKeyDown("space"))
        //     {
        //         LevelLoader.instance.LoadLevel("Level4Part2");
        //     }
        // }
    }

    private void GirlMovement()
    {
        // 到终点
        if (transform.position.x > pointX)
        {
            //transform.localScale = new Vector3();
            IsCollidingSoldier = true;
            Anim.enabled = false;
            IsArrived = true;
            LevelLoader.instance.LoadLevel("Level4Trace");
        }

        if (Input.GetKeyDown("space"))
        {
            Hint.SetActive(false);
            if (IsMoving)
                IsMoving = false;
            else
                IsMoving = true;
        }
        // 继续行走
        if(IsMoving)
        {
            Anim.enabled = true;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        //停止行走
        else
        {
            Anim.enabled = false;
            sprite.sprite = Resources.Load<Sprite>("Level4/GirlHat/A_GirlHatMove00");
            rb.velocity = Vector2.zero;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Soldier2")
        {
            Debug.Log("Soldier2");
            IsCollidingSoldier = true; //撞了士兵
            Anim.enabled = true;
            Anim.SetInteger("Fall", 1);
            StartCoroutine(WaitanimDone());
        }
        else if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
            Anim.enabled = true;
            Anim.SetInteger("Fall", 2);
            StartCoroutine(WaitanimDone());
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(1.2f);
        Restart = true;
    }

    private void FailScreen(string screenname) {
        if (!isEnd) {
            failUI.SetActive(true);     
            Invoke("EndHint",0.7f);
            isEnd = true;
        } else {
            if (restartHint.activeSelf && Input.GetKeyDown("space")) {
                Debug.Log("YES");
                LevelLoader.instance.LoadLevel(screenname);
            }
        }
    }

    void EndHint() {
        restartHint.SetActive(true);
    }

}
