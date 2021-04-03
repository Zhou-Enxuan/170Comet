using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChairRe : MonoBehaviour
{
    public event Action<Collider2D> OnMoveChair;

    private GameObject Hint;
    private GameObject StartTip;
    private float moveH, moveV;
    private GameObject Chair;
    private GameObject Girl;
    private Animator GirlAnim;
    private GameObject Window;
    private Collider2D currentCollider;
    private Collider2D chairCollider;
    public bool touchChair;

    void Awake() {
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
        Chair = GameObject.Find("Chair");
        Girl = GameObject.Find("Girl");
        GirlAnim = Girl.GetComponent<Animator>();
        Window = GameObject.Find("Window");
    }

    void Start()
    {
        Hint.SetActive(false);
        StartTip.SetActive(true);
        touchChair = false;
        chairCollider = null;
    }

    void Update()
    {
        if (chairCollider != null)
        {
            OnMoveChair += HoldChair;
            OnMoveChair?.Invoke(chairCollider);
        }
        else if (currentCollider != null)
        {
            OnMoveChair += MoveInPos;
            OnMoveChair?.Invoke(currentCollider);
        }
        
    }

    void FixedUpdate()
    {

    }

    private void HoldChair(Collider2D other)
    {
        StartTip?.SetActive(false);
        Hint.SetActive(true);

        if (Input.GetKey("space"))
        {
            touchChair = true;
            Chair.SetActive(false);
            GirlAnim.SetBool("chair", true);
            // moveH = Input.GetAxisRaw("Horizontal");
            // moveV = Input.GetAxisRaw("Vertical");
            // GirlAnim.SetFloat("stoolX", moveH);
            // GirlAnim.SetFloat("stoolY", moveV);
            Debug.Log("222222");
        }
        else
        {
            touchChair = false;
            Chair.SetActive(true);
            GirlAnim.SetBool("chair", false);
            Hint.SetActive(false);
            chairCollider = null;
            
        }
        OnMoveChair -= HoldChair;
    }

    private void MoveInPos(Collider2D other)
    {
        if (other.name == "ChairPos")
        {
            Chair.transform.position = new Vector2(1.7f, 0.7f);
            Chair.SetActive(true);
            Hint.SetActive(false);
            Window.GetComponent<OpenWindow>().enabled = true;
            
        }
        OnMoveChair -= MoveInPos;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollider = collision;
        if (collision.name == "Chair")
            chairCollider = collision;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        currentCollider = null;
        if (collision.name == "Chair")
            chairCollider = collision;
    }

}
