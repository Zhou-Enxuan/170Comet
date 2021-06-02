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
        Window.GetComponent<OpenWindow>().enabled = false;
        Window.GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ChairPos")
        {
            Hint.SetActive(false);
            Girl.GetComponent<MoveChairRe>().enabled = false;
            gameObject.transform.position = new Vector2(1.45f, 1.0f);
            gameObject.SetActive(true);
            
            Window.GetComponent<OpenWindow>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<ChairInPosition>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
