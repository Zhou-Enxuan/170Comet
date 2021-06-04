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
    public float screenOffset;

    public float backGroundAreaR;
    public float backGroundAreaL;
    public float buttonAreaR;
    public float buttonAreaL;


    void Awake()
    {
        Button = GameObject.Find("ControlButton");
        BackGround = GameObject.Find("Background");
        
    }

    void Start()
    {
        IsPlayed = false;
        screenOffset = Mathf.Abs(1280 - Screen.currentResolution.width);

        if (Screen.currentResolution.width > 1280)
        {
            backGroundAreaR = BackGround.transform.position.x + BGrt.rect.width / 2 + (screenOffset / 4);
            backGroundAreaL = BackGround.transform.position.x - BGrt.rect.width / 2 - (screenOffset / 4);
            buttonAreaR = Button.transform.position.x + rt.rect.width + (screenOffset / 4);
            buttonAreaL = Button.transform.position.x - rt.rect.width - (screenOffset / 4);
        }
        else
        {
            backGroundAreaR = BackGround.transform.position.x + BGrt.rect.width / 2 - (screenOffset / 4);
            backGroundAreaL = BackGround.transform.position.x - BGrt.rect.width / 2 + (screenOffset / 4);
            buttonAreaR = Button.transform.position.x + rt.rect.width - (screenOffset / 4);
            buttonAreaL = Button.transform.position.x - rt.rect.width + (screenOffset / 4);
        }
    }

    void Update()
    {
        if (transform.position.x < backGroundAreaR && (transform.position.x > backGroundAreaL))
        {
            if (!IsPlayed)
                GetComponent<Image>().enabled = true;
        }
        else{
            GetComponent<Image>().enabled = false;
        }

        //按空格得分--音符消失--音效
        if (transform.position.x < buttonAreaR && transform.position.x > buttonAreaL)
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
