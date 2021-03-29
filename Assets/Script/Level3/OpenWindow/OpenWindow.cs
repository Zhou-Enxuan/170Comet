using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public event Action OnPlayerAction;
    private Animator Anim;
    private GameObject Girl;
    private GameObject Chair;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        Chair = GameObject.Find("Chair");
    }

    void Start()
    {
        Anim.enabled = false;
        
    }

    void Update()
    {
        OnPlayerAction?.Invoke();
    }

    void FixedUpdate()
    {
        
    }

    private void StandOnChair()
    {
        if (Input.GetKeyDown("space"))
        {
            Girl.SetActive(false);
            Chair.SetActive(false);
            Anim.enabled = true;
            StartCoroutine(WaitAnimDone());
        }
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        OnPlayerAction -= StandOnChair;
        OnPlayerAction += OpenWin;
    }

    private void OpenWin()
    {
        if (Input.GetKeyDown("space"))
        {
            Anim.SetBool("Open", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Girl")
            OnPlayerAction += StandOnChair;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Girl")
            OnPlayerAction -= StandOnChair;
    }
}
