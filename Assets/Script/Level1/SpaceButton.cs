using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceButton : MonoBehaviour
{
    [SerializeField] private Sprite ButtonImage;//普通的space
    [SerializeField] private Sprite PressedImage;//按下的space

    void Start()
    {
        GetComponent<Image>().sprite = ButtonImage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")) {
            GetComponent<Image>().sprite = PressedImage;
        }else{
            GetComponent<Image>().sprite = ButtonImage;
        }
    }
}
