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
    private GameObject board1;
    private GameObject board2;
    private GameObject board3;
    private GameObject board4;
    private GameObject board5;
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
        board1 = GameObject.Find("board1");
        board2 = GameObject.Find("board2");
        board3 = GameObject.Find("board3");
        board4 = GameObject.Find("board4");
        board5 = GameObject.Find("board5");
        LeaveTip = GameObject.Find("LeaveTip");
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
        board1.SetActive(false);
        board2.SetActive(false);
        board3.SetActive(false);
        board4.SetActive(false);
        board5.SetActive(false);
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
                    other.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level1/A_Paper");
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
        GameManager.instance.stopMoving = true;
        if (currentBoard == 0)
        {
            board1.SetActive(true);
            PickUpHint.SetActive(false);
            if (board1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                PickUpHint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PickUpHint.SetActive(false);
                    audioSources[0].PlayOneShot(drawingSound, 0.3f);
                    ++currentBoard;
                }
            }
        }
        else if (currentBoard == 1)
        {
            board2.SetActive(true);
            PickUpHint.SetActive(false);
            if (board2.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                PickUpHint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PickUpHint.SetActive(false);
                    audioSources[0].PlayOneShot(drawingSound, 0.3f);
                    ++currentBoard;
                }
            }
        }
        else if (currentBoard == 2)
        {
            board3.SetActive(true);
            PickUpHint.SetActive(false);
            if (board3.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                PickUpHint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PickUpHint.SetActive(false);
                    audioSources[0].PlayOneShot(drawingSound, 0.3f);
                    ++currentBoard;
                }
            }
        }
        else if (currentBoard == 3)
        {
            board4.SetActive(true);
            PickUpHint.SetActive(false);
            if (board4.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                PickUpHint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PickUpHint.SetActive(false);
                    audioSources[0].PlayOneShot(drawingSound, 0.3f);
                    ++currentBoard;
                }
            }
        }
        else if (currentBoard == 4)
        {
            board5.SetActive(true);
            PickUpHint.SetActive(false);
            if (board5.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                PickUpHint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PickUpHint.SetActive(false);
                    ++currentBoard;
                }
            }
        }
        else if (currentBoard > 4)
        {
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
            board4.SetActive(false);
            board5.GetComponent<Animator>().SetTrigger("End");
            // GameManager.instance.stopMoving = false;
            OnCommand -= startDrawing;
            // PickUpHint.SetActive(false);
            StartCoroutine(WaitanimDone());
        }
        
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(1f);
        board5.SetActive(false);
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
