using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlMovement : MonoBehaviour
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
        }
        else
        {
            GirlAnim.SetFloat("x", moveH);
            GirlAnim.SetFloat("y", moveV);
            if (moveH == 0 && moveV == 0)
            {
                rb.velocity = Vector2.zero;
                GirlAnim.enabled = false;
                if (GetComponent<MoveChairRe>().touchChair == false)
                {
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
                else
                {
                    if (tempX == -1)
                    {
                        if (tempY == -1) 
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_WA/A_GirlWithStool_WA_01");//correct:AS -- wrongname:WA
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_A/A_GirlWithStool_A_01");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_SA/A_GirlWithStool_SA_01");//WA -- SA
                    }
                    else if (tempX == 0)
                    {
                        if (tempY == -1)
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_W/A_GirlWithStool_W_01");//S -- W
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_WA/A_GirlWithStool_WA_01");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_S/A_GirlWithStool_S_01");//W -- S
                    }
                    else
                    {
                        if (tempY == -1)
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_WD/A_GirlWithStool_WD_01");//SD -- WD
                        else if (tempY == 0)
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_D/A_GirlWithStool_D_01");
                        else
                            sprite.sprite = Resources.Load<Sprite>("Level3/A_GirlWithStool/A_GirlWithStool_SD/A_GirlWithStool_SD_01");//WD -- SD
                    }
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
