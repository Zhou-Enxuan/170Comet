using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdIMovementNews : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D TSpeed;
    private Rigidbody2D MSpeed;
    private Rigidbody2D BSpeed;
    private float moveH, moveV;
    private Vector2 direction;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 5.0f;
    private GameObject TNews;
    private GameObject MNews;
    private GameObject BNews;
    private GameObject PickUpHint;
    private bool IsOnT = false;
    private bool IsOnM = false;
    private bool IsOnB = false;
    public bool isMove = false;
    
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        TNews = GameObject.Find("TNews");
        MNews = GameObject.Find("MNews");
        BNews = GameObject.Find("BNews");
        TSpeed = TNews.GetComponent<Rigidbody2D>();
        MSpeed = MNews.GetComponent<Rigidbody2D>();
        BSpeed = BNews.GetComponent<Rigidbody2D>();
        PickUpHint = GameObject.Find("PickUpHint");
        isMove = false;
    }


    private void Update(){

        moveH = Input.GetAxisRaw("Horizontal");
        // Debug.Log(Input.GetAxisRaw("Horizontal"));
        moveV = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveH, moveV);
        // Debug.Log(Input.GetAxisRaw("Vertical"));
        rb.velocity = direction * moveSpeed;
        // Debug.Log(rb.velocity);

        if(IsOnT){
            PickUpHint.SetActive(true);

            if (Input.GetKey("space") && rb.velocity.magnitude > 0)
            {
                TSpeed.velocity = rb.velocity;
                Debug.Log("移动第一张报纸");
            }
            else
            {
                TSpeed.velocity = new Vector2(0, 0);
            }
        }else if(IsOnM && TNews.activeSelf == false){
            PickUpHint.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                isMove = true;
            }
            if (Input.GetKey("space") && rb.velocity.magnitude > 0 && isMove)
            {
                MSpeed.velocity = rb.velocity;
                Debug.Log("移动第二张报纸");
            }
            else
            {
                MSpeed.velocity = new Vector2(0, 0);
            }
      
        }else if(IsOnB && TNews.activeSelf == false && MNews.activeSelf == false){
            if (Input.GetKeyDown("space"))
            {
                isMove = true;
            }

            PickUpHint.SetActive(true);
            if( Input.GetKey("space") && isMove)
            {
                BNews.SetActive(false);
            }
        }else{
            PickUpHint.SetActive(false);
        }

        if(BNews.activeSelf == false){
            LevelLoader.instance.LoadLevel("Level2SummerWin");
            GameManager.instance.NewsEnd();
        }

    }

    void OnTriggerEnter2D(Collider2D collision) {


        if(collision.gameObject.name == "TNews"){
            IsOnT =true;
        }

        if(collision.gameObject.name == "MNews"){
            IsOnM =true;
        }

        if(collision.gameObject.name == "BNews"){
            IsOnB = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.name == "TNews"){
            IsOnT = false;
        }

        if(collision.gameObject.name == "MNews"){
            IsOnM = false;
        }

        if(collision.gameObject.name == "BNews"){
            IsOnB = false;
        }
    }

}
