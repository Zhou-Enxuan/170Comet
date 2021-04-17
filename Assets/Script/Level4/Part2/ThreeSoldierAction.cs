using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSoldierAction : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform point;
    private float pointX;
    public float Speed;
    private GameObject Drawing;


    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        pointX = point.position.x;
        Drawing = GameObject.Find("Drawing");
    }

    void Start()
    {
        Destroy(point.gameObject);
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        
        // if (transform.position.x > pointX)
        // {
        //     //transform.localScale = new Vector3();
        //     rb.velocity = Vector2.zero;
        //     GetComponent<Animator>().enabled = false;
        // }

        if (Drawing.GetComponent<Animator>().enabled)
        {
            rb.velocity = Vector2.zero;
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/NPCs/RedSoldier/A_RedSoldier");
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            GetComponent<Animator>().enabled = true;
        }
    }
}
