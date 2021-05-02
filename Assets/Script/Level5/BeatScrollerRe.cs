using System.Collections;
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
    private Vector3 origin;//位置
    public bool Reset;

    void Awake()
    {
        Rhythm = GameObject.Find("RhythmGameUI");
    }

    void Start() 
    {
        origin = transform.position;
    }

    void Update() 
    {
        if(total == 5)
        {
            // if(score >= 4)
            // {
            //     win = true;
            // }
            
            //reset音符位置
            transform.position = origin;
            for(int i = 0; i<5; i++){
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
            }
            musicButtonController.index = 1;
            Reset = true;
        }
        else
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
        }
    }
}
