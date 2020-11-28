using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation1 : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = {"static N", "static NW", "static W", "static SW", "static S", "static SE", "static E,", "static NE"};
    public string[] runDirections = {"run N", "run NW", "run W", "run SW", "run S", "run SE", "run E", "run NE"};

    int lastDirection;
    private void Awake(){
        anim = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 _direction){
        Debug.Log(_direction);

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
