using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingTrigger : MonoBehaviour
{
    private Animator Anim;
    public float num;
    private GameObject Soldier;
    private GameObject Hint;
    private GameObject Girl;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        num = Random.Range(0f, 4.2f);
        Soldier = GameObject.Find("Soldier");
        Hint = GameObject.Find("Hint");
        Girl = GameObject.Find("PlayerGirl");
    }

    void Start()
    {
        Anim.enabled = false;
        Hint.SetActive(false);
    }

    void Update()
    {
        if (!Girl.GetComponent<GirlAction>().IsCollidingSoldier)
        {
            CloseEyes();
        }
        else
        {
            StopAnim();
        }
    }

    private void CloseEyes()
    {
        if (Soldier.transform.position.x > num)
        {
            Anim.enabled = true;
            StartCoroutine(WaitanimDone());
            num = num + Random.Range(5.0f, 10.0f);
            Hint.SetActive(true);
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(5f);
        Anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("temp/bird02");
        Hint.SetActive(true);
    }

    private void StopAnim()
    {
        Anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("temp/bird02");
        Hint.SetActive(false);
    }
}
