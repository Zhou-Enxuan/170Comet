using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInPosition : MonoBehaviour
{
    private GameObject Girl;
    private GameObject Window;
    private GameObject Hint;

    void Awake()
    {
        Girl = GameObject.Find("Girl");
        Window = GameObject.Find("Window");
        Hint = GameObject.Find("Hint");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ChairPos")
        {
            Girl.GetComponent<MoveChairRe>().enabled = false;
            gameObject.transform.position = new Vector2(1.7f, 0.7f);
            gameObject.SetActive(true);
            Hint.SetActive(false);
            Window.GetComponent<OpenWindow>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
