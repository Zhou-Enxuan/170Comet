using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    private bool IsinDoor = false;
    private GameObject LeaveHint;
    private bool IsDialog = true;

    // Start is called before the first frame update
    void Awake()
    {
        LeaveHint  = GameObject.Find("LeaveHint");
    }

    void Start(){
        LeaveHint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsDialogShow()){
            GameManager.instance.stopMoving = true;
        }else{
            GameManager.instance.stopMoving = false;
        }

        if(IsinDoor && !GameManager.instance.stopMoving){
            LevelLoader.instance.LoadLevel("Level3ClimbWall");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsinDoor = true;
            //LeaveHint.SetActive(true);
            if(IsDialog){
                Dialog.PrintDialog("Lv3Part2R");
                IsDialog = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            IsinDoor = false;
            //LeaveHint.SetActive(false);
        }
    }
}
