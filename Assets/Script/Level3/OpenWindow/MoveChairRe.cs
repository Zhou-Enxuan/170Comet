using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChairRe : MonoBehaviour
{
    public event Action<Collider2D> OnMoveChair;
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

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        Chair = GetComponent<SpriteRenderer>();
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
        Girl = GameObject.Find("Girl");
        GirlAnim = Girl.GetComponent<Animator>();
    }

    void Start()
    {
        Hint.SetActive(false);
        InPos = false;
        StartTip.SetActive(true);
        OnMoveChair += HoldChair;
    }

    void Update()
    {
        if (currentCollider != null)
            OnMoveChair?.Invoke(currentCollider);
        
    }

    void FixedUpdate()
    {
        
    }

    private void HoldChair(Collider2D collision)
    {
        if (collision.name == "ChairPos")
        {
            Destroy(rb);
            Hint.SetActive(false);
            OnMoveChair -= HoldChair;
        }
        else{
            if (collision.name == "Girl")
            {
                Hint.SetActive(true);
                StartTip.SetActive(false);
                moveH = Input.GetAxisRaw("Horizontal");
                moveV = Input.GetAxisRaw("Vertical");
                direction = new Vector2(moveH, moveV);
                if (Input.GetKey("space"))
                {
                    OnGirlMove += AnimTrigger;
                    OnGirlMove?.Invoke();
                    if (moveH == 0 && moveV == 0)
                    {
                        rb.isKinematic = true;//不动
                        rb.velocity = Vector2.zero;
                    }
                    else
                    {
                        rb.velocity = direction * moveSpeed;
                        rb.isKinematic = false;
                    }
                }
                else
                {
                    Girl.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    GirlAnim.SetInteger("Direction", 0);
                    OnGirlMove -= AnimTrigger;
                    rb.isKinematic = true;//不动
                    rb.velocity = Vector2.zero;
                }
            }
            else
            {
                Girl.transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Direction", 0);
                rb.isKinematic = true;//不动
                rb.velocity = Vector2.zero;
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
        currentCollider = collision;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        currentCollider = null;
        Hint.SetActive(false);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        // if (collision.name == "ChairPos")
        // {
        //     InPos = true;
        // }
    }
}
