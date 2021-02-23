using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdOutDoorMovement1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Animator birdAnim;
    [SerializeField] private float moveSpeed = 3.0f;
    //private bool IsPickFlower = false;
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        birdAnim = GetComponent<Animator>();

    }

    void Start()
    {
        GameManager.instance.stopMoving = false;
    }

    private void FixedUpdate(){

        birdAnim.SetFloat("height", transform.position.y);
        moveH = Input.GetAxisRaw("Horizontal");
        birdAnim.SetFloat("horizontal", moveH);
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        moveV = Input.GetAxisRaw("Vertical");
        //Debug.Log(Input.GetAxisRaw("Vertical"));
        rb.velocity = new Vector2(moveH, moveV) * moveSpeed;
    }

    

}
