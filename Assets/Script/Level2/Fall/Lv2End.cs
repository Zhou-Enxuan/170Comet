using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2End : MonoBehaviour
{

    void Awake()
    {
    }

    void Start()
    {
        Dialog.PrintDialog("Lv2End");
        StartCoroutine(CheckDialogDone());
    }

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        LevelLoader.instance.LoadLevel("Level3MemoryTL");//ppt3
    }

    void Update()
    {
        
    }

}
