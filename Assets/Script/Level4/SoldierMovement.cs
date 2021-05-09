using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoldierMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;

    private bool moveingRight = true;
    public float waitingTime = 6.0f;
    private float timer = 0.0f;
    private float NPCtimer = 0.0f;

    public Transform groundDe;
    public Transform girlDe;
    public bool ismoving = true;
    public int ignoreLayer = 16;
    public GameObject girl;
    private Animator SoldierAnimator;
    private Animator GreenAnimator;
    private Animator BlackAnimator;
    public GameObject Hat;
    private bool isEnd = false;
    private bool NPCMoveBack = false;
    private bool moveFlag = false;
    private bool IsNpcMoving = false;
    private bool NpcWalkBack = false;

    public GameObject hint;
    public GameObject failUI;
    public GameObject GreenNpC;
    public GameObject BlackNpC;

    private void Start(){
        SoldierAnimator = GetComponent<Animator>();
        GreenAnimator = GameObject.Find("GreenNpC").GetComponent<Animator>();
        BlackAnimator = GameObject.Find("BrownNpc").GetComponent<Animator>();
        hint.SetActive(false);
        failUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(ismoving){
            move();
        }else{
            stop();
        }

        //Debug.Log(girl.GetComponent<GirlOutMovement>().isHiding);

        //LayerMask mask = LayerMask.GetMask("Box1");

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDe.position, Vector2.down, distance);
        RaycastHit2D girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);

        if(moveingRight == true){
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);
            NPCMoveBack = true;
        }else{
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.left, distance);
        }

        if(girlInfo.rigidbody == true && !girl.GetComponent<GirlOutMovement>().isHiding){
            if(girlInfo.rigidbody.name == "PlayerGirl"){
                Debug.Log("Game over");
                if (!isEnd) {
                    failUI.SetActive(true);     
                    Invoke("EndHint",0.7f);
                    isEnd = true;
                } else {
                    if (hint.activeSelf && Input.GetKeyDown(KeyCode.Space)) {
                        LevelLoader.instance.LoadLevel("Level4");
                    }
                }
            }
        }
        

        if(groundInfo.collider.name != "patrolObj"){
            //Debug.Log(groundInfo.collider.name);
            ismoving = false;
            timer += Time.deltaTime;
            if(timer > waitingTime){
                if(moveingRight == true){
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveingRight = false;
                }else{
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveingRight = true;
                    IsNpcMoving = false;
                    NpcWalkBack = true;
                }
                timer = 0f;
                ismoving = true;
            }
        }

        if(girl.GetComponent<GirlOutMovement>().isPickHat){
            Hat.SetActive(false);
        }

    }

    void EndHint() {
        hint.SetActive(true);
    }

    void stop(){
        transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
        GreenNpC.transform.Translate(Vector2.right * 0.5f * Time.deltaTime * 0);
        BlackNpC.transform.Translate(Vector2.right * 0.5f * Time.deltaTime * 0);


        if(moveingRight){//在右边停下，捡帽子，帽子被打下，扔石头.左边停下，播放静止动画
            SoldierAnimator.SetBool("HatDropflag", true);
            BlackAnimator.SetBool("HatDropflag", true);
            GreenAnimator.SetBool("ArgueFlag", true);
            Hat.SetActive(false);
        }else{
            SoldierAnimator.SetBool("Turnflag", true);
            GreenAnimator.SetBool("ArgueFlag", false);
            BlackAnimator.SetBool("WalkFlag", false);
            BlackAnimator.SetBool("WalkBack", false);
            GreenAnimator.SetBool("WalkFlag", false);
            GreenAnimator.SetBool("WalkBack", false);
        }

    }

    void move(){
        //行走，关闭帽子动画或静止动画，行走动画播放。黑帽子静态。绿毛争吵
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        SoldierAnimator.SetBool("HatDropflag", false);
        SoldierAnimator.SetBool("Turnflag", false);

        BlackAnimator.SetBool("HatDropflag", false);

        //当触碰到树的时候，npc向左走，到A停下
        if(IsNpcMoving){//向左走
            BlackNpC.transform.Translate(Vector2.left * 0.5f * Time.deltaTime);
            GreenNpC.transform.Translate(Vector2.left * 0.5f * Time.deltaTime);
            BlackAnimator.SetBool("WalkFlag", true);
            GreenAnimator.SetBool("WalkFlag", true);
        }

        if(moveingRight && NpcWalkBack){//向右走
            BlackNpC.transform.Translate(Vector2.right * 0.75f * Time.deltaTime);
            GreenNpC.transform.Translate(Vector2.right * 0.75f * Time.deltaTime);
            BlackAnimator.SetBool("WalkBack", true);
            GreenAnimator.SetBool("WalkBack", true);
        }else{
            BlackAnimator.SetBool("WalkBack", false);
            GreenAnimator.SetBool("WalkBack", false);
            GreenAnimator.SetBool("ArgueFlag", true);
        }
        Hat.SetActive(true);


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide" && !moveingRight)
        {
            Debug.Log(" go");
            IsNpcMoving = true;
        }else if(collision.gameObject.tag == "Hide" && moveingRight){
            Debug.Log(" stop");
            IsNpcMoving = false;
            NpcWalkBack = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hide")
        {
            //Debug.Log("miss hit box");
            //IsinHideObj = false;
        }
    }

}
