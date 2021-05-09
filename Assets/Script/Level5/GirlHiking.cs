using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlHiking : MonoBehaviour
{
    public event Action OnHiking;

    private Rigidbody2D rb;
    [SerializeField] float Speed;
    [SerializeField] private Transform Dia1, Dia2, Dia3, Dia4;
    [SerializeField] private GameObject Rhythm;
    private float point1, point2, point3, point4;
    private Animator Anim;
    private bool IsMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        point1 = Dia1.position.x;
        point2 = Dia2.position.x;
        point3 = Dia3.position.x;
        point4 = Dia4.position.x;
        Anim = GetComponent<Animator>();
        IsMoving = true;
    }

    void Start()
    {
        OnHiking += Hiking;
        Rhythm.SetActive(true);
    }

    void Update()
    {
        OnHiking?.Invoke();
    }

    private void Hiking()
    {
        if(IsMoving)
        {
            if(transform.position.x > point1 || transform.position.x > point2 || transform.position.x > point3 || transform.position.x > point4)
            {
                rb.velocity = Vector2.zero;
                IsMoving = false;
                GirlStop();
            }
            else
                rb.velocity = new Vector2(Speed, 0);

        }
        
    }

    private void GirlStop()
    {
        Anim.SetTrigger("Stop");
        if (transform.position.x > point1)
        {
            point1 = 50f;
            Dialog.PrintDialog("Lv5Part1");
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point2)
        {
            point2 = 50f;
            Dialog.PrintDialog("Lv5Part2");
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point3)
        {
            point3 = 50f;
            Dialog.PrintDialog("Lv5Part3");
            StartCoroutine(CheckDialogueDone());
        }
        else if (transform.position.x > point4)
        {
            point4 = 50f;
            Dialog.PrintDialog("Lv5Part4");
            StartCoroutine(CheckSceneDone());
        }
    }

    IEnumerator CheckDialogueDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Anim.SetTrigger("Move");
        IsMoving = true;
    }

    IEnumerator CheckSceneDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Debug.Log("The End");
        Anim.enabled = false;
    }
}

