using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingTriggerRe : MonoBehaviour
{
    private Animator Anim;
    private GameObject Drawing;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Drawing = GameObject.Find("Drawing");
    }

    void Start()
    {
        Anim.enabled = false;
    }

    void Update()
    {
        if (Drawing.GetComponent<Animator>().enabled)
        {
            Anim.enabled = true;
        }
        else
        {
            Anim.enabled = false;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level4/HuangGongbg/paint00");
        }
    }

}
