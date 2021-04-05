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
    private float H, V;
    private GameObject Chair;
    private GameObject Girl;
    private Animator GirlAnim;
    private GameObject Window;
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
        H = 0;
        V = 0f;
    }

    void Update()
    {
        if (chairCollider != null)
        {
            OnMoveChair += HoldChair;
            OnMoveChair?.Invoke(chairCollider);
        }
        
    }

    void FixedUpdate()
    {

    }

    private void HoldChair(Collider2D other)
    {
        StartTip?.SetActive(false);
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        if (moveH == 0 && moveV == 0)
        {
        }
        else
        {
            H = moveH;
            V = moveV;
        }

        if (Input.GetKey("space"))
        {
            touchChair = true;
            Chair.SetActive(false);
            GirlAnim.enabled = true;
            GirlAnim.SetBool("chair", true);
            GirlAnim.SetFloat("stoolX", H);
            GirlAnim.SetFloat("stoolY", V);
        }
        else if (Input.GetKeyUp("space"))
        {
            touchChair = false;
            if (other.name == "RoomEdge" || other.name == "ChairPos")
                Chair.transform.position = new Vector2(1.7f, 0.7f);
            else
                Chair.transform.position = this.gameObject.transform.position + new Vector3(H, V - 0.4f, 0f);
            Chair.SetActive(true);
            GirlAnim.SetBool("chair", false);
            chairCollider = null;
        }
        OnMoveChair -= HoldChair;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Chair")
        {
            chairCollider = collision;
            Hint.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Chair")
        {
            chairCollider = collision;
            Hint.SetActive(false);
        }
    }

}
