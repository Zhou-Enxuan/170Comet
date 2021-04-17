using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingTrigger : MonoBehaviour
{
    private Animator Anim;
    public float num;
    private GameObject Soldier;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        num = Random.Range(0f, 4.2f);
        Soldier = GameObject.Find("Soldier");
    }

    void Start()
    {
        Anim.enabled = false;
    }

    void Update()
    {
        CloseEyes();
    }

    private void CloseEyes()
    {
        if (Soldier.transform.position.x > num)
        {
            Anim.enabled = true;
            StartCoroutine(WaitanimDone());
            num = num + Random.Range(5.0f, 10.0f);
        }
    }

    IEnumerator WaitanimDone()
    {
        yield return new WaitForSeconds(5f);
        Anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("temp/bird02");
    }
}
