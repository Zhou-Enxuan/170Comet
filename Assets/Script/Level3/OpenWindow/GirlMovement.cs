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


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        GirlAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveH, moveV);
        rb.velocity = direction * moveSpeed;
        if (moveH != 0 && moveV != 0)
        {
            if (direction.y == 1 && direction.x == -1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementWA");
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 2);
            }

            if (direction.y == 1 && direction.x == 1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementWA");
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 2);
                if (GirlAnim.GetInteger("Direction") == 0)
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (direction.y == -1 && direction.x == -1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementAS");
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 4);

            }

            if (direction.y == -1 && direction.x == 1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementAS");
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 4);
                if (GirlAnim.GetInteger("Direction") == 0)
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (direction.x == -1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementA");
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 3);
            }

            if (direction.x == 1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementA");
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 3);
                if (GirlAnim.GetInteger("Direction") == 0)
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
            }


            if (direction.y == 1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementW");
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 1);
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
            }


            if (direction.y == -1)
            {
                //sprite.sprite = Resources.Load<Sprite>("Level3/GirlMovement/A_GirlMovementS");
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                GirlAnim.SetInteger("Idle", 0);
                GirlAnim.SetInteger("Idle", 5);
            }
        }
    }

}
