using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlGetUp : MonoBehaviour
{
    private Animator Anim;
    private GameObject Girl;
    private GameObject Chair;
    private GameObject Hint;

    void Awake() {
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        Chair = GameObject.Find("Chair");
        Hint = GameObject.Find("Hint");
    }

    void Start()
    {
        Hint.SetActive(false);
        Girl.SetActive(false);
        Girl.GetComponent<MoveChairRe>().enabled = false;
        StartCoroutine(WaitAnimDone());
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        Girl.SetActive(true);
        Girl.GetComponent<MoveChairRe>().enabled = true;
    }

    void Update()
    {

    }

    void FixedUpdate() {
        
    }
}
