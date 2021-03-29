using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChairRe : MonoBehaviour
{
    public event Action OnMoveChair;
    public event Action OnGirlMove;

    private Rigidbody2D rb;
    private SpriteRenderer Chair;
    private float moveH, moveV;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    private GameObject Hint;
    private GameObject StartTip;
    private bool InPos;
    private Collider2D currentCollider;
    private GameObject Girl;
    private Animator GirlAnim;
    private GameObject Window;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        Chair = GetComponent<SpriteRenderer>();
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
        Girl = GameObject.Find("Girl");
        GirlAnim = Girl.GetComponent<Animator>();
        Window = GameObject.Find("Window");
    }

    void Start()
    {
        Hint.SetActive(false);
        InPos = false;
        StartTip.SetActive(true);
        OnMoveChair += HoldChair;
        InPos = false;
    }

    void Update()
    {
        OnMoveChair?.Invoke();
        
    }

    void FixedUpdate()
    {
        
    }

    private void HoldChair()
    {
        if (InPos)
        {
            Destroy(rb);
            Hint.SetActive(false);
            Girl.transform.localRotation = Quaternion.Euler(0, 0, 0);
            GirlAnim.SetInteger("Direction", 0);
            GirlAnim.SetInteger("Idle", 0);
            OnMoveChair -= HoldChair;
            Window.GetComponent<OpenWindow>().enabled = true;
        }
        else{
            if (Hint.activeSelf && Input.GetKey("space"))
            {
                GirlAnim.SetInteger("Direction", 0);
                GirlAnim.SetInteger("Idle", 0);
                OnGirlMove += AnimTrigger;
                OnGirlMove?.Invoke();
                moveH = Input.GetAxisRaw("Horizontal");
                moveV = Input.GetAxisRaw("Vertical");
                direction = new Vector2(moveH, moveV);
                rb.velocity = direction * moveSpeed;
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = true;//不动
                rb.velocity = Vector2.zero;
                Girl.transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Direction", 0);
                GirlAnim.SetInteger("Idle", 0);
                OnGirlMove -= AnimTrigger;
            }
        }
    }

    private void AnimTrigger()
    {
        if (transform.position.x == Girl.transform.position.x)
        {
            if (transform.position.y > Girl.transform.position.y)
                GirlAnim.SetInteger("Direction", 1);
            else if (transform.position.y < Girl.transform.position.y)
                GirlAnim.SetInteger("Direction", 5);
        }
        else if (transform.position.x < Girl.transform.position.x)
        {
            if (transform.position.y == Girl.transform.position.y)
                GirlAnim.SetInteger("Direction", 3);
            else if (transform.position.y > Girl.transform.position.y)
                GirlAnim.SetInteger("Direction", 2);
            else if (transform.position.y < Girl.transform.position.y)
                GirlAnim.SetInteger("Direction", 4);
        }
        else if (transform.position.x > Girl.transform.position.x)
        {
            if (transform.position.y == Girl.transform.position.y)
            {    
                Girl.transform.localRotation = Quaternion.Euler(0, 180, 0);
                GirlAnim.SetInteger("Direction", 3);
                
            }
            else if (transform.position.y > Girl.transform.position.y)
            {
                Girl.transform.localRotation = Quaternion.Euler(0, 180, 0);
                GirlAnim.SetInteger("Direction", 2);
                
            }
            else if (transform.position.y < Girl.transform.position.y)
            {
                Girl.transform.localRotation = Quaternion.Euler(0, 180, 0);
                GirlAnim.SetInteger("Direction", 4);
                
            }
        }
        //Debug.Log(GirlAnim.GetInteger("Direction"));
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Girl" && !InPos)
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
