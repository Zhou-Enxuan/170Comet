using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdOutDoorMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Animator birdAnim;
    [SerializeField] private float moveSpeed = 3.0f;
    private bool IsPickFlower = false;
    private float moveHPrev = -1;
    private bool flipOnce = false;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        birdAnim = GetComponent<Animator>();

    }

    void Start()
    {
        GameManager.instance.stopMoving = false;
    }

    private void FixedUpdate(){
        IsPickFlower = GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower;
        //碰撞到npcone的时候
        birdAnim.SetFloat("height", transform.position.y);
        birdAnim.SetBool("IsPickFlower", IsPickFlower);
        if (!AutoMovement.isPlaCanFly || NpcController.isPlayerMove || GameManager.instance.stopMoving) {
            rb.velocity = Vector2.zero;

            Debug.Log("Stop PlayerMovement");
        } else { //normal时
            moveH = Input.GetAxisRaw("Horizontal");
            birdAnim.SetFloat("horizontal", moveH);
            //Debug.Log(Input.GetAxisRaw("Horizontal"));
            moveV = Input.GetAxisRaw("Vertical");
            //Debug.Log(Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector2(moveH, moveV) * moveSpeed;
            if (moveH == 0 && moveV == 0)
            {
                birdAnim.SetBool("Stand", true);
                if(!flipOnce && birdAnim.GetCurrentAnimatorStateInfo(0).IsName("StaticRest01"))
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
                    flipOnce = true;
                    moveHPrev *= -1;
                }
            }
            else
            {
                birdAnim.SetBool("Stand", false);
                flipOnce = false;
            }
            if(moveH * moveHPrev < 0)
            {
                Debug.Log(moveH * moveHPrev);
                gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
                moveHPrev = moveH;
            }
        }
    }

    

}
