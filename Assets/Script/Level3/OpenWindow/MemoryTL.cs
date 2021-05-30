using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MemoryTL : MonoBehaviour
{
    private GameObject TimeLine;
    private GameObject Notice;
    
    void Awake()
    {
        TimeLine = GameObject.Find("LV3StartTimeline");
        Notice = GameObject.Find("BirdNotice");
    }

    void Start()
    {
        TimelineGameManager.GetDirector(TimeLine.GetComponent<PlayableDirector>());
        TimelineGameManager.isTimeline = true;
        Notice.SetActive(false);
        // SoundManager.playBgm(4);
    }

    void Update()
    {
        
    }

    public void EndMemoryTimeline()
    {
        TimelineGameManager.isTimeline = false;
        TimeLine.GetComponent<PlayableDirector>().enabled = false;
        SceneManager.LoadScene("Level3OpenWindow");
        // LevelLoader.instance.LoadLevel("Level3OpenWindow");
    }
}
