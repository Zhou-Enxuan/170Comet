using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperGameCaller : MonoBehaviour
{

    private bool IsonNewspaper = false;

    private void Start()
    {
        GameManager.instance.stopMoving = false;
    }

    private void Update(){
        if( Input.GetKey("space") && IsonNewspaper == true){
            LevelLoader.instance.LoadLevel("Level2SummerNews");
        }
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name == "Player"){
            IsonNewspaper = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.name == "Player"){
            IsonNewspaper = false;
        }
    }
}
