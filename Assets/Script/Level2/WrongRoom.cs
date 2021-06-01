using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!GameManager.instance.islv2SummerNewsEnd || !GameManager.instance.islv2FallGlassEnd) {
            Dialog.PrintDialog("Lv2WithoutNews");//没拿报纸
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
