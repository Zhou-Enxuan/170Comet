using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryB : MonoBehaviour
{
    public GameObject BNews;

    private void Awake(){
        BNews = GameObject.Find("BNews");
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Destory"){
            SoundManager.playSEOne("paper",0.7f);
            BNews.SetActive(false);
        }
    }
}
