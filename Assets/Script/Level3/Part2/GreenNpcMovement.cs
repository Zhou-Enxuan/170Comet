using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenNpcMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float distance = 5.0f;
    private Animator GAnimator;
    public GameObject talkHint;
    public GameObject ClimbHint;
    public GameObject Girl1;
    public Transform groundDe;
    public Transform girlDe;
    private bool moveingRight = true;
    private float timer = 0.0f;
    public float waitingTime = 0.2f;
    public bool ismoving = true;
    bool isDiaActive = false;
    bool isDiaNeed = false;
    public bool Istalk = false;
    public Animator PAnimator;
    private bool takeTrigger = false;
    private bool giveTrigger = false;
    

    private void Start(){
        GAnimator = GetComponent<Animator>();
        talkHint.SetActive(false);
        ClimbHint.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {


        if(ismoving && !isDiaActive){
            move();
            Debug.Log("move");
        }else if(Istalk){
            MovetoTalk();
            Debug.Log("move to talk");
        }else{
            stop();
            Debug.Log("stop");
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDe.position, Vector2.down, distance);
        RaycastHit2D girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);

        if(moveingRight == true){
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.right, distance);
        }else{
            girlInfo = Physics2D.Raycast(girlDe.position, Vector2.left, distance);
        }

    
        if(groundInfo.collider.name == "G_Background"){
            if(moveingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveingRight = false;
                //Debug.Log("moveleft");
            }else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveingRight = true;
                //Debug.Log("moveright");
            }
        }
        
        
        if(Istalk && !isDiaActive && !isDiaNeed && giveTrigger){
            PAnimator.SetBool("GiveTrigger", true);
            isDiaActive = true;
            isDiaNeed = true;
            talkHint.SetActive(false);
            Dialog.PrintDialog("Lv3Part2");
            GameManager.instance.stopMoving = true;
            StartCoroutine(WaitAnimDone());
        }

        if(takeTrigger && !GameManager.instance.IsDialogShow()){
            PAnimator.SetBool("TakeTrigger", true);
            StartCoroutine(WaitAnimDoneB());
        }
    }


    void move(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        GAnimator.SetBool("DialogFlag", false);
    }

    void stop(){
        transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
        GAnimator.SetBool("DialogFlag", true);
    }

    void MovetoTalk(){
        if(transform.position.x <= 20.5f){
            transform.Translate(Vector2.right * speed * Time.deltaTime * 0);
            GAnimator.SetBool("DialogFlag", true);
            giveTrigger = true;
        }else{
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    IEnumerator WaitAnimDone(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        PAnimator.SetBool("GiveTrigger", false);
        takeTrigger = true;
    }

    IEnumerator WaitAnimDoneB(){
        yield return new WaitWhile(() => PAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        PAnimator.SetBool("TakeTrigger", false);
        GAnimator.SetBool("DialogFlag", false);
        isDiaActive = false;
        takeTrigger = false;
        Istalk = false;
    }

}
