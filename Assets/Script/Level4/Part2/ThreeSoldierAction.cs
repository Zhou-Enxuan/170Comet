using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSoldierAction : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform point;
    private float pointX;
    public float Speed;
    private GameObject Drawing;
    private GameObject Girl;


    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        pointX = point.position.x;
        Drawing = GameObject.Find("Drawing");
        Girl = GameObject.Find("PlayerGirl");
    }

    void Start()
    {
        Destroy(point.gameObject);
        GetComponent<Animator>().SetBool("isWalking", true);
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // 画睁眼 - 不动敬礼
        if (Drawing.GetComponent<Animator>().enabled || Girl.GetComponent<GirlAction>().IsCollidingSoldier || Girl.GetComponent<GirlAction>().IsArrived)
        {
            rb.velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("isWalking", false);
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2/NPCs/RedSoldier/A_RedSoldier");
        }
        // 画闭眼 - 动
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            GetComponent<Animator>().SetBool("isWalking", true);
        }
    }
}
