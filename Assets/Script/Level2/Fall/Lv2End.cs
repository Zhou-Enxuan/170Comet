using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2End : MonoBehaviour
{
    bool isDialogShow;

    void Start()
    {
        isDialogShow = false;
    }

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        LevelLoader.instance.LoadLevel("Level3MemoryTL");//ppt3
    }

    void Update()
    {
        if (GameObject.Find("Loading") == null && !isDialogShow) {
            Dialog.PrintDialog("Lv2End");
            StartCoroutine(CheckDialogDone());
            isDialogShow = true;
        }
    }

}
