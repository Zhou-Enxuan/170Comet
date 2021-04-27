using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2FallStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	GameManager.instance.StorePlayerPos();
        SoundManager.playBgm(5);
        Dialog.PrintDialog("Lv2FallStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
