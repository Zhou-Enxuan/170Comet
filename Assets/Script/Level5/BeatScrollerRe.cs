﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatScrollerRe : MonoBehaviour
{
    [SerializeField] float beatTempo;//音符速度
    [SerializeField] MusicButtonController musicButtonController;
    public int score;//得分
    public int total;//总分
    // public bool win;
    private GameObject Rhythm;
    public Vector3 origin;//位置
    public bool Reset;
    // private GameObject BackGround;
    // [SerializeField] RectTransform BGrt;

    void Awake()
    {
        Rhythm = GameObject.Find("RhythmGameUI");
        // BackGround = GameObject.Find("Background");
    }

    void Start() 
    {
        Debug.Log(Screen.currentResolution.width + " x " + Screen.currentResolution.height);
        origin = transform.position;
        float widthScaler = Screen.currentResolution.width / 16;
        beatTempo /= 1280 / 16;
        beatTempo *= widthScaler;
        //float screenOffset = Mathf.Abs(1280 - 1920);
        //if (1920 > 1280)
        //{
        //    transform.position += new Vector3(screenOffset, 0f, 0f);
        //}
        //else
        //{
        //    transform.position += new Vector3(screenOffset, 0f, 0f);
        //}

    }

    void Update() 
    {
        // for(int i = 0; i<5; i++)
        // {
        //         if (transform.GetChild(i).gameObject.transform.position.x < (BackGround.transform.position.x + BGrt.rect.width/2) && transform.GetChild(i).gameObject.transform.position.x > (BackGround.transform.position.x - BGrt.rect.width/2))
        //         {
        //             Debug.Log(BackGround.transform.position.x + BGrt.rect.width/2);
        //             if (!transform.GetChild(i).gameObject.GetComponent<NoteActions>().IsPlayed)
        //                 transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
        //         }
        //         else{
        //             transform.GetChild(i).gameObject.GetComponent<Image>().enabled = false;
        //         }
        //         // transform.GetChild(i).gameObject.SetActive(true);
        //         // transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
        // }
        

        if(total == 5)
        {
            //reset音符位置
            transform.position = origin;
            for(int i = 0; i<5; i++){
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
            }
            musicButtonController.index = 1;
            if (musicButtonController.index == 1)
                Reset = true;
        }
        else
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
        }
    }
}
