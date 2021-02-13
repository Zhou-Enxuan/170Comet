using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initial : MonoBehaviour
{    void Update()
    {
        LevelLoader.instance.LoadLevel("Level1");
    }
}
