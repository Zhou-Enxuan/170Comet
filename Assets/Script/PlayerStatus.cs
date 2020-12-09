using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update

    public int IsPickPen;
    public int IsPickPaper;

    private void Awake(){
        IsPickPaper = 0;
        IsPickPen = 0;
    }

}
