using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlInGameMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;

    [SerializeField] private float moveSpeed = 3f;
    private Animator GirlAnimator;
    private SpriteRenderer sprite;
    private float tempX;
    private float tempY;
    private bool IsinHideObj;

    public enum girlState {Hiding, UnHiding};
    public static girlState curGirlState;
    public static bool isGirlHiding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnimator = GetComponent<Animator>();
        tempX = 0;
        tempY = 0;
        isGirlHiding = false;
        IsinHideObj = false;
        curGirlState = girlState.UnHiding;
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
        }
        moveH = Input.GetAxisRaw("Horizontal");
        GirlAnimator.SetFloat("Direaction", moveH);
        GirlAnimator.SetFloat("Speed", Mathf.Abs(moveH));

        if(moveH < 0){
            GirlAnimator.SetBool("FaceR", false);
            //Debug.Log("FaceR is " + false);
        }else if(moveH > 0){
            GirlAnimator.SetBool("FaceR", true);
            //Debug.Log("FaceR is " + true);
        }

        direction = new Vector2(moveH, 0);
        rb.velocity = direction * moveSpeed;  

        if (IsinHideObj) {
        	if(curGirlState == girlState.UnHiding  && Input.GetKeyDown("space")) {
        		sprite.sortingOrder = -2;
        		curGirlState = girlState.Hiding;
        		isGirlHiding = true;
        	} else if (curGirlState == girlState.Hiding && Input.GetKeyDown("space")) {
        		sprite.sortingOrder = 0;
        		curGirlState = girlState.UnHiding;
        		IsinHideObj = false;
        	}
        } else {
			sprite.sortingOrder = 0;
            curGirlState = girlState.UnHiding;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("hit box");
            IsinHideObj = true;
        }
	}

	void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("miss hit box");
            IsinHideObj = false;
        }
    }
}
