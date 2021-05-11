using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    private bool IsinDoor = false ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsinDoor && Input.GetKeyDown(KeyCode.Space)){
            LevelLoader.instance.LoadLevel("Level3ClimbWall");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            IsinDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            IsinDoor = false;
        }
    }
}
