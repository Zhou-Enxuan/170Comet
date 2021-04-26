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
    private GameObject Continue;
    private GameObject Crossfade;

    private void Awake()
    {
        Crossfade = GameObject.Find("Crossfade");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
        pointX = EndPoint.position.x;
        Continue = GameObject.Find("Continue");
    }

    void Start()
    {
        IsMoving = true;
        IsCollidingSoldier = false;
        Destroy(EndPoint.gameObject);
        IsArrived = false;
        Continue.SetActive(false);
    }


    void Update() 
    {
        if (!IsCollidingSoldier)
        {
            GirlMovement();
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void GirlMovement()
    {
        // 到终点
        if (transform.position.x > pointX)
        {
            //transform.localScale = new Vector3();
            IsCollidingSoldier = true;
            IsArrived = true;
            //fade out黑屏
            StartCoroutine(LoadLevel());
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
            GetComponent<Animator>().enabled = true;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        //停止行走
        else
        {
            GetComponent<Animator>().enabled = false;
            sprite.sprite = Resources.Load<Sprite>("A_Girl");
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator LoadLevel()
	{
		Crossfade.GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(1f);
        //进入Level4Part3
        Application.Quit();
		//SceneManager.LoadScene("");
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Soldier2")
        {
            Debug.Log("Soldier2");
            IsCollidingSoldier = true; //撞了士兵
            //fade out黑屏
            StartCoroutine(ReloadLevel());
        }
        if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
            //fade out黑屏
            StartCoroutine(ReloadLevel());
        }
    }

    IEnumerator ReloadLevel()
	{
		Crossfade.GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Level4Part2");
	}

}
