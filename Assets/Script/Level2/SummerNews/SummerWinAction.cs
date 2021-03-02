using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerWinAction : MonoBehaviour
{
    [SerializeField]
    private Transform destination;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.stopMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < destination.position.y)
        {
            transform.position += new Vector3(0, speed / 1000, 0);
        }
        else
        {
            GameManager.instance.stopMoving = false;
            Debug.Log(transform.position + ": " + destination.position);
        }
    }
}
