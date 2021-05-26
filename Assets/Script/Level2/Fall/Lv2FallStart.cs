using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2FallStart : MonoBehaviour
{
    private GameObject Happy;
    private GameObject Telescope;

    void Awake()
    {
        Happy = GameObject.Find("Happy");
        Telescope = GameObject.Find("Telescope");
    }

    void Start()
    {
        Happy.SetActive(false);
        Telescope.SetActive(true);
    	GameManager.instance.StorePlayerPos();
        SoundManager.playBgm(5);
        GameManager.instance.stopMoving = true;
        Dialog.PrintDialog("Lv2FallStart1");
        StartCoroutine(CheckDialogDone());
    }

    void Update()
    {
        
    }

    IEnumerator CheckDialogDone()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Happy.SetActive(true);
        Telescope.SetActive(false);
        Dialog.PrintDialog("Lv2FallStart2");
        StartCoroutine(CheckDialog2Done());
    }

    IEnumerator CheckDialog2Done()
    {
        yield return new WaitWhile(GameManager.instance.IsDialogShow);
        Happy.SetActive(false);
    }
}
