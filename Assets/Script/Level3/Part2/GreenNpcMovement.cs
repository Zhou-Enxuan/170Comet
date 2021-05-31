using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenNpcMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;
    private Animator GAnimator;
    // public GameObject talkHint;
    public GameObject ClimbHint;
    public GameObject player;
    // public Transform groundDe;
    // public Transform girlDe;
    private bool moveingRight = false;
    private float timer = 0.0f;
    public float waitingTime = 0.2f;
    public static bool ismoving;
    public static bool Istalk;
    public Animator PAnimator;
    public static bool takeTrigger = false;
 
    public Transform[] movePos;
    private int posNum;
    private float turnWaitTime;
    private float wait;
    private bool isFirstTalk;

    private void Start(){
        GAnimator = GetComponent<Animator>();
        //talkHint.SetActive(false);
        ClimbHint.SetActive(false);

        turnWaitTime = 0.1f;
        wait = turnWaitTime;
        Istalk = false;
        ismoving = true;
        isFirstTalk = true;
    }

    // Update is called once per frame
    void Update() {

        //if (ismoving && !GameManager.instance.IsDialogShow()) {
        if (ismoving) {
           move();
            // if (Istalk) {
            //     Dialog.PrintDialog("Lv3Part2");
            //     // GameManager.instance.stopMoving = true;
            //     PAnimator.SetBool("GiveTrigger", true);
            //     //StartCoroutine(WaitAnimDone());
            //     ismoving = false;
            // }
        } else if (Istalk) {
            if (!GAnimator.GetBool("DialogFlag")) {
                MovetoTalk();
            } 
            else {
                if (isFirstTalk && !GameManager.instance.IsDialogShow()) {
                    isFirstTalk = false;
                    Dialog.PrintDialog("Lv3Part2");
                    PAnimator.SetBool("GiveTrigger", true);
                } else if (!isFirstTalk && !GameManager.instance.IsDialogShow()){
                    Dialog.PrintDialog("Lv3Part202");
                    Istalk = false;
                }
            }
        }

        //结束对话，收玻璃
        if (takeTrigger && !GameManager.instance.IsDialogShow()) {
            Debug.Log("收回");
            Istalk = false;
            GameManager.instance.stopMoving = true;
            PAnimator.SetBool("TakeTrigger", true);
            // StartCoroutine(WaitAnimDoneB());
        }
    }

    // void Update()
    // {

    //     Debug.Log("now moving" + ismoving);

    //     // 玩家拿出玻璃后收回玻璃
    //     if(takeTrigger && !GameManager.instance.IsDialogShow()){
    //         PAnimator.SetBool("TakeTrigger", true);
    //         StartCoroutine(WaitAnimDoneB());
    //     }
    

    //     if(ismoving && !isDiaActive){
    //         move();
    //     }else if(Istalk){
    //         MovetoTalk();
    //     }else{
    //         stop();
    //     }

    //     if(Istalk && !isDiaActive && !isDiaNeed){
    //                 Debug.Log("in obj");
    //                 isDiaActive = true;
    //                 isDiaNeed = true;
    //                 talkHint.SetActive(false);
    //                 Dialog.PrintDialog("Lv3Part2");
    //                 GameManager.instance.stopMoving = true;
    //                 PAnimator.SetBool("GiveTrigger", true);
    //                 StartCoroutine(WaitAnimDone());
    //             }
    //     // RaycastHit2D groundInfo = Physics2D.Raycast(groundDe.position, Vector2.down, distance);
    //     // RaycastHit2D girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);

    //     // if(moveingRight == true){
    //     //     girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);
    //     // }else{
    //     //     girlInfo = Physics2D.Raycast(girlDe.position, Vector2.left, distance);
    //     // }

    //     // if(girlInfo.rigidbody == true){
    //     //     if(girlInfo.rigidbody.name == "GirlDe"){
    //     //     }
    //     // }
        


    //     // if(groundInfo.collider.name == "G_Background"){
    //     //     Debug.Log(groundInfo.collider.name);
    //     //     //Debug.Log("not in obj");
    //     //     ismoving = false;
    //     //         if(moveingRight == true){
    //     //             transform.eulerAngles = new Vector3(0, -180, 0);
    //     //             moveingRight = false;
    //     //             Debug.Log("moveleft");
    //     //         }else{
    //     //             transform.eulerAngles = new Vector3(0, 0, 0);
    //     //             moveingRight = true;
    //     //             Debug.Log("moveright");
    //     //         }
    //     //     ismoving = true;
    //     // }else{
    //     //         Debug.Log("in obj");
    //     // }    

        
    // }

// 来回走
    void move() {
        if (moveingRight) {
            posNum = 1;
        } else {
            posNum = 0;
        }
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        GAnimator.SetBool("DialogFlag", false);
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y), speed * Time.deltaTime);
        if (Vector2.Distance(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y)) < 0.1f) {
            if (turnWaitTime > 0) {
                turnWaitTime -= Time.deltaTime;
            } 
            else {
                if (moveingRight) {
                    Debug.Log("转右");
                    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                    moveingRight = false;
                } 
                //正在向左走，需要转身
                else {
                    Debug.Log("转左");
                    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                    moveingRight = true;
                }
                turnWaitTime = wait;
            }
        }    
    }

    // void stop(){
    //     transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
    //     GAnimator.SetBool("DialogFlag", true);
    // }

    void MovetoTalk(){
        // 有距离
        if (player.transform.position.x >= this.transform.position.x + 1.5f || player.transform.position.x <= this.transform.position.x - 1.5f ) {
            Debug.Log("判定转身once");
            //npc向右走，位置大于玩家位置
            //if (!GAnimator.GetBool("DialogFlag")) {
                if (moveingRight && this.transform.position.x > player.transform.position.x) {
                    PAnimator.SetBool("FaceR", true);
                    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                    moveingRight = false;
                } 
                else if (moveingRight && this.transform.position.x < player.transform.position.x) {
                    PAnimator.SetBool("FaceR", false);
                }
                else if (!moveingRight && this.transform.position.x > player.transform.position.x) {
                    PAnimator.SetBool("FaceR", true);
                }
                else if (!moveingRight && this.transform.position.x < player.transform.position.x) {
                    PAnimator.SetBool("FaceR", false);
                    this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
                    moveingRight = true;
                }
                GAnimator.SetBool("DialogFlag", true);
            //}
        }
        // 走两步 拉开距离
        else {
            if (moveingRight) {
                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x + 2f, this.transform.position.y), speed * Time.deltaTime);
            }
            else {
               this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x - 2f, this.transform.position.y), speed * Time.deltaTime); 
            }
        }
        //Debug.Log("movetotalk");
        // if (transform.position.x <= 20.5f) {
        //     GAnimator.SetBool("DialogFlag", true);
        //     //Debug.Log("stop");
        // }
        // else{
        //     if (moveingRight) {
        //         this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        //         moveingRight = false;
        //     }
        //     this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(20.5f, this.transform.position.y), speed * Time.deltaTime);
        // }
    }
    // 掏出玻璃
    IEnumerator WaitAnimDone(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        PAnimator.SetBool("GiveTrigger", false);
        takeTrigger = true;
        Debug.Log("掏出");
    }
    // 收起玻璃
    IEnumerator WaitAnimDoneB(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        PAnimator.SetBool("TakeTrigger", false);
        GAnimator.SetBool("DialogFlag", false);
        GameManager.instance.stopMoving = false;
        takeTrigger = false;
        Debug.Log("收回玻璃");
    }
//     public float speed = 3.0f;
//     public float distance = 5.0f;
//     private Animator GAnimator;
//     public GameObject talkHint;
//     public GameObject ClimbHint;
//     public GameObject Girl1;
//     public Transform groundDe;
//     public Transform girlDe;
//     private bool moveingRight = true;
//     private float timer = 0.0f;
//     public float waitingTime = 0.2f;
//     public bool ismoving = true;
//     bool isDiaActive = false;
//     bool isDiaNeed = false;
//     public bool Istalk = false;
//     public Animator PAnimator;
//     private bool takeTrigger = false;
    

//     private void Start(){
//         GAnimator = GetComponent<Animator>();
//         talkHint.SetActive(false);
//         ClimbHint.SetActive(false);
//     }


//     // Update is called once per frame
//     void Update()
//     {

//         Debug.Log("now moving" + ismoving);


//         if(takeTrigger && !GameManager.instance.IsDialogShow()){
//             PAnimator.SetBool("TakeTrigger", true);
//             StartCoroutine(WaitAnimDoneB());
//         }
    

//         if(ismoving && !isDiaActive){
//             move();
//         }else if(Istalk){
//             MovetoTalk();
//         }else{
//             stop();
//         }

//         RaycastHit2D groundInfo = Physics2D.Raycast(groundDe.position, Vector2.down, distance);
//         RaycastHit2D girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);

//         if(moveingRight == true){
//             girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);
//         }else{
//             girlInfo = Physics2D.Raycast(girlDe.position, Vector2.left, distance);
//         }

//         if(girlInfo.rigidbody == true){
//             if(girlInfo.rigidbody.name == "GirlDe"){
//             }
//         }
        


//         if(groundInfo.collider.name == "G_Background"){
//             Debug.Log(groundInfo.collider.name);
//             //Debug.Log("not in obj");
//             ismoving = false;
//                 if(moveingRight == true){
//                     transform.eulerAngles = new Vector3(0, -180, 0);
//                     moveingRight = false;
//                     //Debug.Log("moveleft");
//                 }else{
//                     transform.eulerAngles = new Vector3(0, 0, 0);
//                     moveingRight = true;
//                     //Debug.Log("moveright");
//                 }
//             ismoving = true;
//         }else{
//                 //Debug.Log("in obj");
//         }
        
            

//         if(Istalk && !isDiaActive && !isDiaNeed){
//             Debug.Log("in obj");
//             isDiaActive = true;
//             isDiaNeed = true;
//             talkHint.SetActive(false);
//             Dialog.PrintDialog("Lv3Part2");
//             GameManager.instance.stopMoving = true;
//             PAnimator.SetBool("GiveTrigger", true);
//             StartCoroutine(WaitAnimDone());
//         }
//     }


//     void move(){
//         transform.Translate(Vector2.right * speed * Time.deltaTime);
//         GAnimator.SetBool("DialogFlag", false);
//     }

//     void stop(){
//         transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
//         GAnimator.SetBool("DialogFlag", true);
//     }

//     void MovetoTalk(){
//         Debug.Log("movetotalk");
//         if(transform.position.x <= 20.5f){
//             transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
//             GAnimator.SetBool("DialogFlag", true);
//             Debug.Log("stop");
//         }else{
//             transform.Translate(Vector2.right * speed * Time.deltaTime);
//         }
//     }

//     IEnumerator WaitAnimDone(){
//         yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
//         //transform.Translate(Vector2.left * speed * Time.deltaTime);
//         PAnimator.SetBool("GiveTrigger", false);
//         takeTrigger = true;
//     }

//     IEnumerator WaitAnimDoneB(){
//         yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
//         PAnimator.SetBool("TakeTrigger", false);
//         GAnimator.SetBool("DialogFlag", false);
//         isDiaActive = false;
//         takeTrigger = false;
//     }

}
