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
    private GameObject SpaceHint;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        Chair = GameObject.Find("Chair");
        SpaceHint = GameObject.Find("SpaceHint");
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
            if (SpaceHint.activeSelf)
                SpaceHint.SetActive(false);
            Girl.SetActive(false);
            Chair.SetActive(false);
            Anim.enabled = true;
            StartCoroutine(WaitAnimDone());
        }
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        SpaceHint.SetActive(true);
        OnPlayerAction -= StandOnChair;
        OnPlayerAction += OpenWin;
    }

    private void OpenWin()
    {
        if (Input.GetKeyDown("space"))
        {
            SpaceHint.SetActive(false);
            Anim.SetBool("Open", true);
            LevelLoader.instance.LoadLevel("Level4");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Girl" && Chair.transform.position == new Vector3(1.45f, 1.0f, 0f))
        {
            OnPlayerAction += StandOnChair;
            SpaceHint.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Girl" && Chair.transform.position == new Vector3(1.45f, 1.0f, 0f))
        {
            OnPlayerAction -= StandOnChair;
            SpaceHint.SetActive(false);
        }
    }
}
