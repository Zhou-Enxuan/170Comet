using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongRoom : MonoBehaviour
{
    private GameObject Hint;

    void Awake()
    {
        Hint = GameObject.Find("Hint");
    }

    void Start()
    {
        Hint.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Level2WaitingRoom" && !GameManager.instance.islv2SummerNewsEnd) {
            Dialog.PrintDialog("Lv2WithoutNews");
        }
        else if (SceneManager.GetActiveScene().name == "Level2FallWaitingRoom" && !GameManager.instance.islv2FallGlassEnd ) {
            Dialog.PrintDialog("Lv2WithoutNews");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
