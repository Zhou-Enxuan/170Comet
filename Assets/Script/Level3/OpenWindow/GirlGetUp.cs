using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlGetUp : MonoBehaviour
{

    private Animator Anim;
    private GameObject Girl;
    private GameObject Chair;

    void Awake() {
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        Chair = GameObject.Find("Chair");
    }

    void Start()
    {
        Girl.SetActive(false);
        Chair.GetComponent<MoveChairRe>().enabled = false;
        StartCoroutine(WaitAnimDone());
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        Girl.SetActive(true);
        Chair.GetComponent<MoveChairRe>().enabled = true;
    }

    void Update()
    {

    }

    void FixedUpdate() {
        
    }
}
