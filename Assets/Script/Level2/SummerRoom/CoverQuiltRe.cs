using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverQuiltRe : MonoBehaviour
{
    public event Action OnQuiltMovement;

    private Animator Anim;
    private GameObject Player;
    private GameObject KeyHint;

    void Awake() 
    {
        Anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        KeyHint = GameObject.Find("KeyHint");
    }

    void Start()
    {
        //Player.GetComponent<QuiltCaller>().enabled = false;
        Player.SetActive(false);
        Anim.enabled = true;
        OnQuiltMovement += CoveringQuilt;
    }

    void Update()
    {
        OnQuiltMovement?.Invoke();
    }

    void FixedUpdate()
    {
        
    }

    private void CoveringQuilt()
    {
        if (!Anim.GetBool("Cover3") && Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                //按上触发第一段
                if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                    Anim.SetBool("Cover1", true);
                }
                //按左触发第二段
                else if (Anim.GetBool("Cover1")) {
                    Anim.SetBool("Cover1", false);
                    Anim.SetBool("Cover2", true);
                }
                //按下触发第三段
                else if (Anim.GetBool("Cover2")) {
                    Anim.SetBool("Cover2", false);
                    Anim.SetBool("Cover3", true); 
                }
                
            }
        
            else if (Input.GetKeyDown(KeyCode.DownArrow)){
                //按上触发第一段
                if (!Anim.GetBool("Cover1") && !Anim.GetBool("Cover2") && !Anim.GetBool("Cover3")) {
                }
                //按左触发第二段
                else if (Anim.GetBool("Cover1")) {
                    Anim.SetBool("Cover1", false);
                }
                //按下触发第三段
                else if (Anim.GetBool("Cover2")) {
                    Anim.SetBool("Cover2", false);
                    Anim.SetBool("Cover1", true);
                }
            }
        }
        else if (Anim.GetBool("Cover3"))
        {
            Anim.SetBool("Cover3", false);
            StartCoroutine(WaitCoveranimDone());
        }
    }

    IEnumerator WaitCoveranimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        Anim.SetBool("End", true);
        KeyHint.SetActive(false);
        //Player.GetComponent<QuiltCaller>().enabled = true;
        //Player.transform.position = new Vector2(-3.7f, 1f);
        Player.SetActive(true);
        
        OnQuiltMovement -= CoveringQuilt;
    }
}
