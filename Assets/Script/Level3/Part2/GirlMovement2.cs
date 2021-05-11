using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlMovement2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH;
    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1.0f;
    private Animator GirlAnimator;
    private bool isnearWall;
    public GameObject ClimbHint;
    private bool FaceR;
    // Start is called before the first frame update
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        GirlAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
        }else{
        moveH = Input.GetAxisRaw("Horizontal");
        GirlAnimator.SetFloat("Direaction", moveH);
        GirlAnimator.SetFloat("Speed", Mathf.Abs(moveH));

        //Debug.Log("speed is " + Mathf.Abs(moveH));
        //Debug.Log("direaction is " + moveH);

        if(moveH < 0){
            GirlAnimator.SetBool("FaceR", false);
            //Debug.Log("FaceR is " + false);
            FaceR = false;
        }else if(moveH > 0){
            GirlAnimator.SetBool("FaceR", true);
            //Debug.Log("FaceR is " + true);
            FaceR = true;
        }
        direction = new Vector2(moveH, 0);
        rb.velocity = direction * moveSpeed;
        }

        if(isnearWall){
            ClimbHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && FaceR){
                GirlAnimator.SetBool("ClimbTrigger", true);
                StartCoroutine(WaitAnimDone());
            }
        }else{
            ClimbHint.SetActive(false);
        }
    }

    IEnumerator WaitAnimDone(){
        yield return new WaitWhile(() => GirlAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        GirlAnimator.SetBool("ClimbTrigger", false);
        transform.position = new Vector3(26.84f, -1.69f, -0.6602975f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            isnearWall = true;
            Debug.Log("Wall");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            isnearWall = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Boundery")
        {
            LevelLoader.instance.LoadLevel("Level4");
        }
    }
}
