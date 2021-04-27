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
    private float moveHPrev;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        birdAnim = GetComponent<Animator>();

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level2SummerWin")
        {
            birdAnim.SetBool("IsNews", true);
        }
        else
        {
            birdAnim.SetBool("IsNews", false);
        }

        if(GetComponent<SpriteRenderer>().flipX)
        {
            moveHPrev = 1;
        }
        else
        {
            moveHPrev = -1;
        }
    }

    private void FixedUpdate(){
        IsPickFlower = GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower;
        //碰撞到npcone的时候
        birdAnim.SetFloat("height", transform.position.y);
        birdAnim.SetBool("IsPickFlower", IsPickFlower);
        if (GameManager.instance.stopMoving) {
            rb.velocity = Vector2.zero;

            Debug.Log("Stop PlayerMovement");
            birdAnim.SetBool("Stand", true);
        }
        else { //normal时
            moveH = Input.GetAxisRaw("Horizontal");
            birdAnim.SetFloat("horizontal", moveH);
            //Debug.Log(Input.GetAxisRaw("Horizontal"));
            moveV = Input.GetAxisRaw("Vertical");
            //Debug.Log(Input.GetAxisRaw("Vertical"));
            rb.velocity = new Vector2(moveH, moveV) * moveSpeed;
            if (moveH == 0 && moveV == 0)
            {
                birdAnim.SetBool("Stand", true);
            }
            else
            {
                birdAnim.SetBool("Stand", false);
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
