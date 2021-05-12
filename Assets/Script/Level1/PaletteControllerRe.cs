using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteControllerRe : MonoBehaviour
{
    public event Action OnCommand;

    public event Action<Collider2D> OnPlayerAction;

    private GameObject QuestionMark;
    private GameObject PickUpHint;
    private GameObject Rpaper;
    private GameObject Rpen;
    private GameObject paper;
    private GameObject pen;
    private GameObject pen_paper;
    private GameObject board;
    private Animator boardAnim;
    private GameObject LeaveTip;
    private Collider2D currentCollider;
    private int currentBoard = 0;
    private AudioSource[] audioSources;
    private AudioClip drawingSound;
    private AudioClip penSound;
    private AudioClip paperSound;
    private int girlItem = 0;


    private void Awake()
    {
        QuestionMark = GameObject.Find("GirlQMark");
        PickUpHint = GameObject.Find("PickUpHint");
        pen_paper = GameObject.Find("BubblePenPaper");
        pen = GameObject.Find("BubblePen");
        paper = GameObject.Find("BubblePaper");
        Rpen = GameObject.Find("Pen");
        Rpaper = GameObject.Find("Paper");
        board = GameObject.Find("palette1");
        LeaveTip = GameObject.Find("LeaveTip");
        boardAnim = board.GetComponent<Animator>();
        drawingSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_DrawingSound");
        penSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_PenSound");
        paperSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_PaperSound");
        SoundManager.playBgm(1);
    }

    void Start()
    {
        OnPlayerAction += FirstDialog;
        OnPlayerAction += Itemcheck;
        LeaveTip.SetActive(false);
        pen_paper.SetActive(false);
        paper.SetActive(false);
        pen.SetActive(false);
        board.SetActive(false);
        PickUpHint.SetActive(false);
        audioSources = this.gameObject.GetComponents<AudioSource>();
    }

    void Update()
    {
        if (currentCollider != null)
            OnPlayerAction?.Invoke(currentCollider);
        OnCommand?.Invoke();
    }

    private void FirstDialog(Collider2D other)
    {
        if (other.tag == "trigger")
        {
            PickUpHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PickUpHint.SetActive(false);
                Dialog.PrintDialog("Level1 Start");
                QuestionMark.SetActive(false);
                StartCoroutine(ShowBubble());
                OnPlayerAction -= FirstDialog;
            }
        }
    }

    IEnumerator ShowBubble()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        pen_paper.SetActive(true);
        OnPlayerAction += turnItemToGirl;
    }

    private void Itemcheck(Collider2D other)
    {
        if(other.tag == "pen" || other.tag == "paper")
        {
            PickUpHint.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(other.tag == "pen")
                {
                    other.gameObject.SetActive(false);
                    audioSources[1].PlayOneShot(penSound, 0.3f);
                    if (GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PAPER)
                    {
                        Rpaper.SetActive(true);
                        Rpaper.transform.position = transform.position;
                    }
                    GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.PEN;

                }
                else if(other.tag == "paper")
                {
                    other.gameObject.SetActive(false);
                    audioSources[2].PlayOneShot(paperSound, 0.3f);
                    if (GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PEN)
                    {
                        Rpen.SetActive(true);
                        Rpen.transform.position = transform.position;
                    }
                    GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.PAPER;
                }
            }
        }
    }

    private void turnItemToGirl(Collider2D collision)
    {
        Debug.Log("in_turnItemToGirl");
        if (collision.tag == "trigger" && (GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PEN || GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PAPER))
        {
            PickUpHint.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space) )
            {
                ++girlItem;

                if (girlItem > 1)
                {
                    pen_paper.SetActive(false);
                    pen.SetActive(false);
                    paper.SetActive(false);
                    GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;

                    Rpaper.transform.position = new Vector2(-2.12f, 1f);
                    Rpaper.GetComponent<BoxCollider2D>().enabled = false;
                    Rpaper.SetActive(true);
                    Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                    Rpen.GetComponent<BoxCollider2D>().enabled = false;
                    Rpen.SetActive(true);
                    OnPlayerAction -= turnItemToGirl;
                    OnCommand += startDrawing;
                    return;

                }
                else
                {
                    if (GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PAPER)
                    { 
                        pen_paper.SetActive(false);
                        pen.SetActive(true);
                        PickUpHint.SetActive(false);
                        Rpaper.transform.position = new Vector2(-2.12f, 1f);
                        Rpaper.GetComponent<BoxCollider2D>().enabled = false;
                        Rpaper.SetActive(true);
                        GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
                    }
                    else if (GetComponent<BirdInDoorMovement>().currentState == BirdInDoorMovement.BirdsState.PEN)
                    {
                        pen_paper.SetActive(false);
                        paper.SetActive(true);
                        PickUpHint.SetActive(false);
                        Rpen.transform.position = new Vector2(-2.89f, 1.6f);
                        Rpen.GetComponent<BoxCollider2D>().enabled = false;
                        Rpen.SetActive(true);
                        GetComponent<BirdInDoorMovement>().currentState = BirdInDoorMovement.BirdsState.STATIC;
                    }
                }
            }
        }
    }

    private void startDrawing()
    {
        board.SetActive(true);
        GameManager.instance.stopMoving = true;
        if (Input.GetKeyDown(KeyCode.Space) && boardAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            boardAnim.SetInteger("current", ++currentBoard);
            if (currentBoard > 4)
            {
                GameManager.instance.stopMoving = false;
                OnCommand -= startDrawing;
                PickUpHint.SetActive(false);
                StartCoroutine(WaitanimDone());
            }
            else
            {
                audioSources[0].PlayOneShot(drawingSound, 0.3f);
            }
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.stopMoving = false;
        SceondDialog();
    }

    public void SceondDialog()
    {
        Dialog.PrintDialog("Level1 End");
        StartCoroutine(CheckDialogDone());
    }

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        LeaveTip.SetActive(true);
        OnPlayerAction += WindowHint;

    }

    private void WindowHint(Collider2D collision)
    {
        if (collision.name == "Window")
        {
            LeaveTip.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PickUpHint.SetActive(false);
                LeaveTip.SetActive(false);
                GameObject.Find("Player").GetComponent<BirdInDoorMovement>().Numdirection = 0;
                GameManager.instance.stopMoving = true;
                GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
                GameObject.Find("Player").GetComponent<Animator>().enabled = true;
                OnPlayerAction -= WindowHint;
                StartCoroutine(waitFlyAnimOver());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollider = collision;
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    OnPlayerAction?.Invoke(collision);
    //}

    void OnTriggerExit2D(Collider2D collision)
    {
        PickUpHint.SetActive(false);
        LeaveTip.SetActive(false);
        currentCollider = null;
        //LeaveTip.SetActive(false);
    }

    IEnumerator waitFlyAnimOver()
    {
        SoundManager.playSEOne("birdFlyOut", 0.7f);
        yield return new WaitWhile(() => GameObject.Find("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        LevelLoader.instance.LoadLevel("Level2Winter");
    }
}
