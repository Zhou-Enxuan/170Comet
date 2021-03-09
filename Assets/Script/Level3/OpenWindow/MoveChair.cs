using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChair : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer Chair;
    private float moveH, moveV;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    private GameObject Hint;
    private GameObject StartTip;
    public static bool InPos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Chair = GetComponent<SpriteRenderer>();
        Hint = GameObject.Find("Hint");
        Hint.SetActive(false);
        InPos = false;
        StartTip = GameObject.Find("StartTip");
        StartTip.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (InPos)
        {
            Destroy(rb);
            Hint.SetActive(false);
        }
        else
        {
            if (Hint.activeSelf && Input.GetKey("space"))
            {
               
                moveH = Input.GetAxisRaw("Horizontal");
                moveV = Input.GetAxisRaw("Vertical");
                direction = new Vector2(moveH, moveV);
                rb.velocity = direction * moveSpeed;
                if (moveH != 0 && moveV != 0)
                {
                    if (direction.y == 1 && direction.x == -1)
                        //transform.localRotation = Quaternion.Euler(0, 180, 0);

                    if (direction.y == 1 && direction.x == 1)
                        transform.localRotation = Quaternion.Euler(0, 0, 0);

                    if (direction.y == -1 && direction.x == -1)
                        //transform.localRotation = Quaternion.Euler(0, 180, 0);

                    if (direction.y == -1 && direction.x == 1)
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    if (direction.x == -1)
                        //transform.localRotation = Quaternion.Euler(0, 180, 0);

                    if (direction.x == 1)
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = true;//不动
                rb.velocity = Vector2.zero;
            }

            }//else
       
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Girl")
        {
            Hint.SetActive(true);
            StartTip.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Girl")
        {
            Hint.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "ChairPos")
        {
            InPos = true;
        }
    }
}
