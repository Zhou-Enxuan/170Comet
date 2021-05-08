using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlHiking : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float Speed;
    [SerializeField] Transform Dia1;
    [SerializeField] Transform Dia2;
    [SerializeField] Transform Dia3;
    [SerializeField] Transform Dia4;
    private float pointX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pointX = Dia1.position.x;
    
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x > pointX)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(Speed, 0);
        }
    }
}

