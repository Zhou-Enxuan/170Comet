using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScrollerRe : MonoBehaviour
{
    [SerializeField] float beatTempo;
    public int score;
    public int total;
    public bool win;
    private GameObject Rhythm;

    void Awake()
    {
        Rhythm = GameObject.Find("RhythmGameUI");
    }

    void Start() 
    {

    }

    void Update() 
    {
        if(total == 5)
        {
            if(score >= 4)
            {
                win = true;
            }

            // Rhythm.SetActive(false);
        }
        else
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
        }
    }
}
