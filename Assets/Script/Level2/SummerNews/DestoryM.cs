using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryM : MonoBehaviour
{
    public GameObject MNews;

    private void Awake(){
        MNews = GameObject.Find("MNews");
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Destory"){
            SoundManager.playSEOne("paper",0.7f);
            MNews.SetActive(false);
            GameObject.Find("Player_News").GetComponent<BirdIMovementNews>().isMove = false;
        }
    }
}
