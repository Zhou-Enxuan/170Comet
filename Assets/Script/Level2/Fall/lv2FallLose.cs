using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv2FallLose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Transform>().position = GameManager.instance.PlayerPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
