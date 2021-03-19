using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChairRe : MonoBehaviour
{
    public event Action OnMoveChair;

    private Rigidbody2D rb;
    private SpriteRenderer Chair;
    private float moveH, moveV;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    private GameObject Hint;
    private GameObject StartTip;
    private bool InPos;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        Chair = GetComponent<SpriteRenderer>();
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
    }

    void Start()
    {
        Hint.SetActive(false);
        InPos = false;
        StartTip.SetActive(true);
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Girl")
        {
            Hint.SetActive(true);
            StartTip.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Girl")
        {
            Hint.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "ChairPos")
        {
            InPos = true;
        }
    }
}
