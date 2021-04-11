using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlOutMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 1.0f;
    private Animator GirlAnimator;
    private float tempX;
    private float tempY;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnimator = GetComponent<Animator>();
        tempX = 0;
        tempY = 0;
    }

    void Start()
    {
    }


    void Update() 
    {
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
        }
        moveH = Input.GetAxisRaw("Horizontal");
        GirlAnimator.SetFloat("Direaction", moveH);
        GirlAnimator.SetFloat("Speed", Mathf.Abs(moveH));

        Debug.Log("speed is " + Mathf.Abs(moveH));
        Debug.Log("direaction is " + moveH);


        direction = new Vector2(moveH, 0);
        rb.velocity = direction * moveSpeed;  
    }

}
