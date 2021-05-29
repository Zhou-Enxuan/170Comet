using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GirlAction : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator Anim;
    private GameObject Hint;
    private SpriteRenderer sprite;
    public float Speed;
    private bool IsMoving;
    private GameObject Drawing;
    public bool IsCollidingSoldier;
    //public Transform EndPoint;
    //private float pointX;
    public static bool IsArrived = false;
    private bool isFailDialog;
    private GameObject KeyHint;

    public GameObject failUI;
    public GameObject restartHint;
    private bool isEnd;
    private bool Restart;
    public GameObject gsoldier;
    public static GameObject girlKingTimeLine;

    private void Awake()
    {
        SoundManager.playBgm(6);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        Hint = GameObject.Find("Hint");
        Drawing = GameObject.Find("Drawing");
        //pointX = EndPoint.position.x;
        girlKingTimeLine = GameObject.Find("GirlKingTL");
        KeyHint = GameObject.Find("KeyHint");

    }

    void Start()
    {
        TimelineGameManager.GetDirector(girlKingTimeLine.GetComponent<PlayableDirector>());
        girlKingTimeLine.SetActive(false);
        gsoldier.SetActive(false);
        IsMoving = true;
        IsCollidingSoldier = false;
        //Destroy(EndPoint.gameObject);
        IsArrived = false;
        failUI.SetActive(false);
        restartHint.SetActive(false);
        isEnd = false;
        Restart = false;
        isFailDialog = false;
        if(GamePlaySystemManager.isLevel4Part2Failed)
        {
            this.gameObject.GetComponent<StartDialog>().enabled = false;
            KeyHint.SetActive(true);
        }else{
            KeyHint.SetActive(false);
        }
    }


    void Update() 
    {
        if(!this.gameObject.GetComponent<StartDialog>().enabled && Input.anyKeyDown)
        {
            KeyHint.SetActive(false);
        }
        if (!IsCollidingSoldier)
        {
            GirlMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (IsArrived && !GameManager.instance.IsDialogShow() && !girlKingTimeLine.activeSelf){
                TimelineGameManager.isTimeline = true;
                Destroy(GameObject.Find("G_Soliders"));
                Anim.SetBool("isWalking",false);
                gsoldier.SetActive(true);
                girlKingTimeLine.SetActive(true);
            }
                
            if(girlKingTimeLine.GetComponent<PlayableDirector>().enabled == false){
                    LevelLoader.instance.LoadLevel("Level4P2TL3");
            }

            if (Restart)
            {
                FailScreen("Level4Part2");
            }
        }
        // if (Hint.activeSelf){
        //     if (Input.GetKeyDown("space"))
        //     {
        //         LevelLoader.instance.LoadLevel("Level4Part2");
        //     }
        // }
    }

    IEnumerator CheckDialog2Done()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        GamePlaySystemManager.isLevel4Part2Failed = true;
        failUI.SetActive(true);     
        Invoke("EndHint",0.7f);
    }

    private void GirlMovement()
    {
        // 到终点
        if (IsArrived)
        {
            IsCollidingSoldier = true;
            IsMoving = false;
            Dialog.PrintDialog("Lv4Part2Follow");
            GameManager.instance.StorePlayerPos();
            //StartCoroutine(CheckDialogueDone());
        }

        if (Input.GetKey("space") && !this.gameObject.GetComponent<StartDialog>().enabled)
        {
            Hint.SetActive(false);
            IsMoving = false;
            // if (IsMoving)
            //     IsMoving = false;
            // else
            //     IsMoving = true;
        }else if (Input.GetKeyUp("space")){
            Hint.SetActive(false);
            IsMoving = true;
        }
        // 继续行走
        if(IsMoving)
        {
            Anim.enabled = true;
            Anim.SetBool("isWalking",true);
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        //停止行走
        else
        {
            Anim.SetBool("isWalking",false);
           //sprite.sprite = Resources.Load<Sprite>("Level4/GirlHat/A_GirlHatMove00");
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator CheckDialogueDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        //对话播完进入level4Trace，缺少timeline
        LevelLoader.instance.LoadLevel("Level4Trace");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Soldier2")
        {
            Debug.Log("Soldier2");
            IsCollidingSoldier = true; //撞了士兵
            Anim.SetInteger("Fall", 1);
            StartCoroutine(WaitanimDone());
        }
        else if(collision.name == "Soldier3")
        {
            Debug.Log("Soldier3");
            IsCollidingSoldier = true;//撞了士兵
            Anim.SetInteger("Fall", 2);
            StartCoroutine(WaitanimDone());
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(1.2f);
        Restart = true;
        isFailDialog = true;
    }

    private void FailScreen(string screenname) {
        if (isFailDialog){
            Dialog.PrintDialog("Lv4Part2Fail");
            StartCoroutine(CheckDialog2Done());
            isFailDialog = false;
        // }
        // if (!isEnd) {
        //     failUI.SetActive(true);     
        //     Invoke("EndHint",0.7f);
        //     isEnd = true;
        } else {
            if (restartHint.activeSelf && Input.GetKeyDown("space")) {
                Debug.Log("YES");
                LevelLoader.instance.LoadLevel(screenname);
            }
        }
    }

    void EndHint() {
        restartHint.SetActive(true);
    }

    public void Actived() {
        Destroy(GameObject.Find("G_SoldierTL"));
    }
    public void changeSprite() {
        Anim.SetTrigger("isTookOff");
    }
    public void kingGrab() {
        GameObject.Find("King").GetComponent<Animator>().SetTrigger("isGrabed");
    }
    public void kingThrow() {
        GameObject.Find("King").GetComponent<Animator>().SetTrigger("isThrowed");
    }
}
