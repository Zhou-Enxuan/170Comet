using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlLeavebed : MonoBehaviour
{
    private Animator Anim;
    private GameObject Girl;
    private GameObject Hint;
    private GameObject SpaceHint;
    public float _timeHeld = 0.0f;

    void Awake() {
        Anim = GetComponent<Animator>();
        Girl = GameObject.Find("Girl");
        SpaceHint = GameObject.Find("SpaceHint");
    }

    void Start()
    {
        Girl.SetActive(false);
        SpaceHint.SetActive(true);
        Anim.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
             _timeHeld = 0f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
         _timeHeld += Time.deltaTime;
        }
        
        if(_timeHeld >= 2.0f){
            SpaceHint.SetActive(false);
            Anim.enabled = true;
            StartCoroutine(WaitAnimDone());
        }
    }

    IEnumerator WaitAnimDone()
    {
        yield return new WaitWhile(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        Girl.SetActive(true);
        gameObject.GetComponent<GirlLeavebed>().enabled = false;
    }

}
