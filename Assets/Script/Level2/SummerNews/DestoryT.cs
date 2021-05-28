using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryT : MonoBehaviour
{
    public GameObject TNews;
    public bool isDestroy = false;

    private void Awake(){
        TNews = GameObject.Find("TNews");

    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Destory"){
            SoundManager.playSEOne("paper",0.7f);
            TNews.SetActive(false);
        }
    }
}
