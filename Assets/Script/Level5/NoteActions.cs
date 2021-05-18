using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteActions : MonoBehaviour
{
    [SerializeField] MusicButtonController musicButtonController;
    [SerializeField] int thisIndex;
    private GameObject Button;
    [SerializeField] RectTransform rt;
    [SerializeField] BeatScrollerRe beatScrollerRe;
    [SerializeField] AudioSource PressSound;
    public bool IsPlayed;//CHECK each note only hit ONCE
    private GameObject BackGround;
    [SerializeField] RectTransform BGrt;

    void Awake()
    {
        Button = GameObject.Find("ControlButton");
        BackGround = GameObject.Find("Background");
    }

    void Start()
    {
        IsPlayed = false;
    }

    void Update()
    {
        if (transform.position.x < (BackGround.transform.position.x + BGrt.rect.width/2) && transform.position.x > (BackGround.transform.position.x - BGrt.rect.width/2))
        {
            if (!IsPlayed)
                GetComponent<Image>().enabled = true;
        }
        else{
            GetComponent<Image>().enabled = false;
        }

        //按空格得分--音符消失--音效
        if (transform.position.x < (Button.transform.position.x + rt.rect.width/2) && transform.position.x > (Button.transform.position.x - rt.rect.width/2))
        {
            if(Input.GetKeyDown("space"))
            {
                
                if(musicButtonController.index == thisIndex && !IsPlayed)
                {
                    PressSound.Play();
                    beatScrollerRe.score += 1;
                    GetComponent<Image>().enabled = false;
                    IsPlayed = true;
                }
            }
            
        }
        else if (transform.position.x < 0)
        {
            beatScrollerRe.total += 1;
            IsPlayed = false;
            this.gameObject.SetActive(false);
        }
    }

}
