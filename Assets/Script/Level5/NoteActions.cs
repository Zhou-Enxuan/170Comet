using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteActions : MonoBehaviour
{
    [SerializeField] MusicButtonController musicButtonController;
    [SerializeField] int thisIndex;
    private GameObject Button;
    [SerializeField] RectTransform rt;
    [SerializeField] BeatScrollerRe beatScrollerRe;

    void Awake()
    {
        Button = GameObject.Find("ControlButton");
    }

    void Start()
    {
    }

    void Update()
    {
        //按空格得分--音符消失--音效
        if (transform.position.x < (Button.transform.position.x + rt.rect.width/2) && transform.position.x > (Button.transform.position.x - rt.rect.width/2))
        {
            if(musicButtonController.index == thisIndex)
            {
                if(Input.GetKeyDown("space"))
                {
                    beatScrollerRe.score += 1;
                    beatScrollerRe.total += 1;
                    this.gameObject.SetActive(false);
                }
            }
            
        }
        else if (transform.position.x < 0)
        {
            beatScrollerRe.total += 1;
            this.gameObject.SetActive(false);
        }
    }
}
