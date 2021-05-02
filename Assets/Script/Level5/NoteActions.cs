using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteActions : MonoBehaviour
{
    [SerializeField] MusicButtonController musicButtonController;
    [SerializeField] float thisIndex;
    private GameObject Button;
    [SerializeField] RectTransform rt;
    [SerializeField] BeatScrollerRe beatScrollerRe;
    private Vector3 origin; 

    void Awake()
    {
        Button = GameObject.Find("ControlButton");
    }

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        if (beatScrollerRe.total == 5)
        {
            transform.position = origin;
            this.gameObject.SetActive(true);
        }

        if (transform.position.x < (Button.transform.position.x + rt.rect.width/2) && transform.position.x > (Button.transform.position.x - rt.rect.width/2))
        {
            if(musicButtonController.index == thisIndex && Input.GetKeyDown("space"))
            {
                beatScrollerRe.score += 1;
                this.gameObject.SetActive(false);
            }
        }
        else if (transform.position.x < 0)
        {
            beatScrollerRe.total += 1;
            this.gameObject.SetActive(false);
        }
    }
}
