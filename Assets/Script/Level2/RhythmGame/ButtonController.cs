using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite ButtonPic;
    [SerializeField] Sprite PressedPic;
    private int index;
    private SpriteRenderer SR;
    private GameObject Notes;

    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        Notes = GameObject.Find("Notes");
    }

    void Start()
    {
        index = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            SR.sprite = PressedPic;
        }else{
            SR.sprite = ButtonPic;
        }

        if ((index < 5 && Input.GetKeyDown("down")) || (index < 5 && Input.GetKeyDown("s")))
        {
            ++index;
            
        }
        else if ((index > 1 && Input.GetKeyDown("up")) || (index > 1 && Input.GetKeyDown("w")))
        {
            --index;
        }

        transform.position = new Vector3(transform.position.x, Notes.transform.GetChild(index-1).gameObject.transform.position.y, 0);

    }

    void OnTriggerEnter2D(Collider2D other) {

    }

    void OnTriggerExit2D(Collider2D other) {

    }
}
