using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteActions : MonoBehaviour
{
    [SerializeField] MusicButtonController musicButtonController;
    [SerializeField] float thisIndex;
    private GameObject Button;
    [SerializeField] RectTransform rt;

    void Awake()
    {
        Button = GameObject.Find("ControlButton");
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (musicButtonController.index == thisIndex)
        {
            if (transform.position.x < (Button.transform.position.x + rt.rect.width/2) && transform.position.x > (Button.transform.position.x - rt.rect.width/2))
            {
                if(Input.GetKeyDown("space"))
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
