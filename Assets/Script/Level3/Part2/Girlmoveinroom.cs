using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlmoveinroom : MonoBehaviour
{

    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 1.0f;
    private Animator GirlAnim;
    private float tempX;
    private float tempY;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnim = GetComponent<Animator>();
        tempX = 0;
        tempY = 0;
    }

    void Start()
    {
    }


    void Update() 
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveH, moveV);
        

        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
            GirlAnim.SetFloat("x", 0);
            GirlAnim.SetFloat("y", 0);
            GirlAnim.enabled = false;
        }
        else
        {
            GirlAnim.SetFloat("x", moveH);
            GirlAnim.SetFloat("y", moveV);
            if (moveH == 0 && moveV == 0)
            {
                Debug.Log("stop");
                rb.velocity = Vector2.zero;
                GirlAnim.enabled = false;
                    if (tempX == -1)
                    {
                        if (tempY == -1) 
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementAS");
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementA");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementWA");
                    }
                    else if (tempX == 0)
                    {
                        if (tempY == -1)
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementS");
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementAS");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementW");
                    }
                    else
                    {
                        if (tempY == -1)
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementSD");
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementD");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementWD");
                    }
            }
            else{
                GirlAnim.enabled = true;
                rb.velocity = direction * moveSpeed;
                tempX = GirlAnim.GetFloat("x");
                tempY = GirlAnim.GetFloat("y");
            }
        }
    }

}

