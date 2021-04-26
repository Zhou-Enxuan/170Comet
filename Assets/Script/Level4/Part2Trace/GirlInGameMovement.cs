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
    private bool IsinHideObj;
    private bool isPickBird;
    public enum girlState {Hiding, UnHiding,CantControl};
    public static girlState curGirlState;
    public static bool isGirlHiding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnimator = GetComponent<Animator>();
        isGirlHiding = false;
        IsinHideObj = false;
        isPickBird = false;
        curGirlState = girlState.UnHiding;
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (KingControl.isToNextScene && GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
            if (KingControl.sceneCount == 0) {
            	AutoMove(14f);
            } else if (KingControl.sceneCount == 1) {
            	AutoMove(-3f);
            } else if (KingControl.sceneCount == 2) {
            	Debug.Log("通过通过通过通过");
            }
        }
        else
        {
        	this.GetComponent<BoxCollider2D>().isTrigger = false;

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
	        	if (curGirlState == girlState.UnHiding  && Input.GetKeyDown("space")) {
	        		if (isPickBird) {
	        			KingControl.birdItem.SetActive(false);
	        		} 
	        		else {
		        		sprite.sortingOrder = -2;
		        		curGirlState = girlState.Hiding;
		        		isGirlHiding = true;
	        		}
	        	} else if (curGirlState == girlState.Hiding && Input.GetKeyDown("space")) {
	        		if (isPickBird) {
	        			KingControl.birdItem.SetActive(false);
	        		} 
	        		else {
		        		sprite.sortingOrder = 0;
		        		curGirlState = girlState.UnHiding;
		        		IsinHideObj = false;
		        	}
	        	}
	        } else {
				sprite.sortingOrder = 0;
	            curGirlState = girlState.UnHiding;
	        	if (isPickBird && Input.GetKeyDown("space")) {
					KingControl.birdItem.SetActive(false);
	        	}
	        }
    	}
    }

    void AutoMove(float targetPoint) {
		this.GetComponent<BoxCollider2D>().isTrigger = true;
		if (Vector2.Distance(this.transform.position, new Vector2(targetPoint, this.transform.position.y)) < 0.1f) {
	    	this.transform.position = new Vector2(targetPoint, this.transform.position.y);
	    	GirlAnimator.Play("Stand");
	    	GirlAnimator.SetFloat("Speed", 0f);
		} 
		else
	    {
			this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(targetPoint, this.transform.position.y), moveSpeed * Time.deltaTime);
			GirlAnimator.SetFloat("Speed", Mathf.Abs(1));
	    	GirlAnimator.SetBool("FaceR", false);
			
		}
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("hit box");
            IsinHideObj = true;
        }
        if (collision.gameObject.name == "Bird") {
        	isPickBird = true;
        }
        if (KingControl.isToNextScene && KingControl.sceneCount == 0 && collision.gameObject.name == "InvisibleWall03") {
        	GameManager.instance.stopMoving = true;
        }
        else if (KingControl.isToNextScene && KingControl.sceneCount == 1 && collision.gameObject.name == "InvisibleWall02") {
        	GameManager.instance.stopMoving = true;
        }
        else if (KingControl.isToNextScene && KingControl.sceneCount == 2 && collision.gameObject.name == "InvisibleWall01") {
        	GameManager.instance.stopMoving = true;
        }
	}

	void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("miss hit box");
            IsinHideObj = false;
        }

        if (collision.gameObject.name == "Bird") {
        	isPickBird = false;
        }
    }
}
