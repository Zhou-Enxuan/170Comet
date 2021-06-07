using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo;//音符速度
    private GameObject Button;

    public int score;//玩家得分
    public int total;//游戏总分
    public Vector3 origin;//音符初始位置
    public bool Reset = false;

    void Awake() 
    {
        Button = GameObject.Find("MusicButton");
    }

    void Start()
    {
        beatTempo = beatTempo/60f;
        origin = transform.position;
        score = 0;
        total = 0;
    }

    void Update() {
        if (total == 5)
        {
            transform.position = origin;
            Reset = true;
        }else{
            transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
        }

        // if(total == 5)
        // {
        //     //reset音符位置
        //     transform.position = origin;
        //     for(int i = 0; i<5; i++){
        //         transform.GetChild(i).gameObject.SetActive(true);
        //         // transform.GetChild(i).gameObject.GetComponent<Image>().enabled = true;
        //     }
        //     // musicButtonController.index = 1;
        //     // if (musicButtonController.index == 1)
        //         Reset = true;
        // }
        // else
        // {
            // transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
        // }


        // if (CanBeTriggered)
        // {
        //     if (Input.GetKeyDown("space"))
        //     {
        //         this.gameObject.SetActive(false);
        //         score += 1;
        //         total += 1;
        //         beatTempo = 0;
        //     }
        // }
        // else if (transform.position.x < BackGround.transform.position.x)
        // {
        //     // this.gameObject.SetActive(false);
        //     total += 1;
        // }
    }

    
}
