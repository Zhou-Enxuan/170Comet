using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiltCaller : MonoBehaviour
{
    public event Action<Collider2D> OnPlayerAction;
    public event Action OnPlayerFinish;

    private Animator Anim;
    private Collider2D currentCollider;
    private GameObject BedWQuilt;
    private GameObject Player;
    private GameObject SpaceHint;
    private GameObject KeyHint; //盖被子的提示1
    private GameObject GirlQMark;
    private SpriteRenderer SR;
    private bool LBed;
    //private GameObject Window;
    //private GameObject LeaveHint;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        BedWQuilt = GameObject.Find("BedWQuilt");
        Player = GameObject.Find("Player");
        SpaceHint = GameObject.Find("SpaceHint");
        GirlQMark = GameObject.Find("GirlQMark");
        KeyHint = GameObject.Find("KeyHint");
        LBed = false;
        //Window = GameObject.Find("Window");
        //LeaveHint = GameObject.Find("LeaveHint");
        SR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SpaceHint.SetActive(false);
        GirlQMark.SetActive(true);
        KeyHint.SetActive(false);
        BedWQuilt.GetComponent<Animator>().enabled = false;
        OnPlayerAction += GoToBed;
    }

    void Update()
    {
        if (currentCollider != null)
        {
            OnPlayerAction?.Invoke(currentCollider);
        }
        OnPlayerFinish?.Invoke();
        if (BedWQuilt.GetComponent<Animator>().GetBool("End") && Player.activeSelf)
        {
            OnPlayerFinish += LeaveBed;
        }
    }
    
    void FixedUpdate()
    {
        
    }

    private void GoToBed(Collider2D other)
    {
        if (other.name == "BedWQuilt")
        {  
            Debug.Log("GoToBed");
            SpaceHint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                GirlQMark.SetActive(false);
                KeyHint.SetActive(true);
                Anim.enabled = true;
                Player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Anim.SetBool("IsGoingBed", true);
                OnPlayerAction -= GoToBed;
                StartCoroutine(WaitanimDone());
            }
        }
        
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //GameManager.instance.stopMoving = true;
        //SR.enabled = false;
        BedWQuilt.GetComponent<CoverQuiltRe>().enabled = true;
        //OnPlayerFinish += LeaveBed;
        //Player.transform.position = new Vector2(-3.7f, 1f);
        
    }

    private void LeaveBed()
    {
        Player.transform.position = new Vector2(-3.7f, 0.1f);
        Anim.enabled = true;
        Anim.SetBool("IsLeavingBed", true);
        //SR.enabled = true;
        OnPlayerFinish -= LeaveBed;
        StartCoroutine(WaitLeaveanimDone());
    }

    IEnumerator WaitLeaveanimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        //GameManager.instance.stopMoving = false;
        Anim.SetBool("IsLeavingBed", false);
        Player.GetComponent<QuiltCaller>().enabled = false;
        //Anim.enabled = false;
        //Anim.SetBool("IsFlyingOut", true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollider = collision;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        SpaceHint.SetActive(false);
        currentCollider = null;
    }
}
