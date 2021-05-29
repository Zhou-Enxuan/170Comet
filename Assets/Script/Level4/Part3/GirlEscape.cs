using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GirlEscape : MonoBehaviour
{
    private GameObject TimeLine1;
    private GameObject TimeLine2;
    private GameObject Girl;
    private Animator GirlAnim;
    private Animator BrownAnim;

    void Awake()
    {
        TimeLine1 = GameObject.Find("GirlFallTimeline");
        TimeLine2 = GameObject.Find("PalaceTimeline");
        Girl = GameObject.Find("PlayerGirl");
        GirlAnim = Girl.GetComponent<Animator>();
        BrownAnim = GameObject.Find("BrownMan").GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        TimelineGameManager.GetDirector(TimeLine1.GetComponent<PlayableDirector>());
        TimelineGameManager.isTimeline = true;
        GirlAnim.SetTrigger("Run");
        GirlAnim.SetTrigger("Fall");
        TimeLine2.SetActive(false);
        SoundManager.playBgm(8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTimeline1()
    {
        TimelineGameManager.isTimeline = false;
        TimeLine1.GetComponent<PlayableDirector>().enabled = false;
        Girl.GetComponent<RunInPalace>().enabled = true;
    }
}
