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
        Dialog.PrintDialog("Lv3Start");
        StartCoroutine(CheckDialogDone());
    }

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        AccessNextLevel();
    }

    void Update()
    {
        
    }

    private void AccessNextLevel()
    {
        if (Input.GetKeyDown("space"))
        {
            LevelLoader.instance.LoadLevel("Level3MemoryTL");
        }
    }
}
