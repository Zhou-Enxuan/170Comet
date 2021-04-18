using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
    }

    void Start()
    {
        IsMoving = true;
        IsCollidingSoldier = false;
    }


    void Update() 
    {
        if (!IsCollidingSoldier)
            GirlMovement();
        else
        {
            GetComponent<Animator>().enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void GirlMovement()
    {
        if (Input.GetKeyDown("space"))
        {
            Hint.SetActive(false);
            if (IsMoving)
                IsMoving = false;
            else
                IsMoving = true;
        }
        if(IsMoving)
        {
            GetComponent<Animator>().enabled = true;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Soldier2")
        {
            Debug.Log("Soldier2");
            IsCollidingSoldier = true; //撞了士兵
        }
        if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
        }
    }

}
