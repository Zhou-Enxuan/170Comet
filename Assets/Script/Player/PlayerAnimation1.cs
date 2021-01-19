using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation1 : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E,", "Static_NE"};
    public string[] runDirections = {"Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE"};

    int lastDirection;
    private void Awake(){
        anim = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 _direction){

        string[] directionArray = null;

        if(_direction.magnitude < 0.01){
            directionArray = staticDirections;
        }else{
            directionArray = runDirections;

            lastDirection = DirectionToIndex(_direction);
        }
        anim.Play(directionArray[lastDirection]);
    }

    public int DirectionToIndex(Vector2 _direction){
        Vector2 norDir = _direction.normalized;

        float step = 360/8;
        float offset = step/2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if(angle < 0){
            angle +=360;
        }

        float stepCount = angle/step;
        return Mathf.FloorToInt(stepCount);
    }
    
}
