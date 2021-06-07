using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlGetUp : MonoBehaviour
{
    private Animator Anim;
    private GameObject Girl;
    private GameObject Chair;
    private GameObject Hint;
    private GameObject StartTip;
    private GameObject SpaceHint;

    void Awake() {
        SoundManager.playBgm(12);
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        Chair = GameObject.Find("Chair");
        Hint = GameObject.Find("Hint");
        StartTip = GameObject.Find("StartTip");
        SpaceHint = GameObject.Find("SpaceHint");
    }

    void Start()
    {
        Hint.SetActive(false);
        Girl.SetActive(false);
        StartTip.SetActive(false);
        Girl.GetComponent<MoveChairRe>().enabled = false;
        Anim.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SpaceHint.SetActive(false);
            Anim.enabled = true;
            StartCoroutine(WaitAnimDone());
        }
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        StartTip.SetActive(true);
        Girl.SetActive(true);
        Girl.GetComponent<MoveChairRe>().enabled = true;
        gameObject.GetComponent<GirlGetUp>().enabled = false;
    }

}
