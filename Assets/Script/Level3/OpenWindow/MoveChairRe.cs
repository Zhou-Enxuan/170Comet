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
    private GameObject SpaceHint;
    private bool collideChair;
    private GameObject Table;

    void Awake() {
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
        Chair = GameObject.Find("Chair");
        Girl = GameObject.Find("Girl");
        GirlAnim = Girl.GetComponent<Animator>();
        Window = GameObject.Find("Window");
        SpaceHint = GameObject.Find("SpaceHint");
        Table = GameObject.Find("Table");
    }

    void Start()
    {
        Hint.SetActive(false);
        touchChair = false;
        chairCollider = null;
        collideChair = false;
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
        if (StartTip.activeSelf)
            StartTip.SetActive(false);
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        

        if (Input.GetKey("space") && collideChair)
        {
            touchChair = true;
            Chair.SetActive(false);
            if (moveH == 0 && moveV == 0)
            {
            }
            else
            {
                H = moveH;
                V = moveV;
                GirlAnim.enabled = true;
                GirlAnim.SetBool("chair", true);
                GirlAnim.SetFloat("stoolX", H);
                GirlAnim.SetFloat("stoolY", V);
            }
        }
        else if (Input.GetKeyUp("space") && collideChair)
        {
            touchChair = false;
            if (other.name == "ChairPos")
                Chair.transform.position = new Vector2(1.45f, 1.0f);
            else if (Chair.GetComponent<Renderer>().bounds.Intersects(GameObject.Find("Bed").GetComponent<Renderer>().bounds))
                Chair.transform.position = gameObject.transform.position;
            else if (Chair.GetComponent<Renderer>().bounds.Intersects(Table.GetComponent<Renderer>().bounds))
                Chair.transform.position = gameObject.transform.position;
            else
                Chair.transform.position = this.gameObject.transform.position + new Vector3(H * 0.8f, V * 0.8f - 0.5f, 0f);
            
            Chair.SetActive(true);
            GirlAnim.SetBool("chair", false);
            chairCollider = null;
            collideChair = false;
        }
        OnMoveChair -= HoldChair;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Chair" && Chair.transform.position != new Vector3(1.45f, 1.0f, 0f))
        {
            chairCollider = collision;
            Hint.SetActive(true);
            collideChair = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Chair"  && Chair.transform.position != new Vector3(1.45f, 1.0f, 0f))
        {
            chairCollider = collision;
            Hint.SetActive(false);
            if (!touchChair)
                collideChair = false;
        }
    }

}
