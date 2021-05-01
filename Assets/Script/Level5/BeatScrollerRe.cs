using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScrollerRe : MonoBehaviour
{
    [SerializeField] float beatTempo;

    void Start() 
    {

    }

    void Update() 
    {
        transform.position -= new Vector3(beatTempo * Time.deltaTime *2, 0f, 0f);
    }
}
