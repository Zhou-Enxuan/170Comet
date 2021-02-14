using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdOutDoorMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Animator birdAnim;
    [SerializeField] private float moveSpeed = 1.0f;
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        birdAnim = GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.instance.stopMoving = false;
    }

    private void FixedUpdate(){
        //碰撞到npcone的时候
        birdAnim.SetFloat("height", transform.position.y);
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
        }
    }

    

}
