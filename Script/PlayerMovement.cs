using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    //basic variables
    public float move_speed;
    private bool facing_right = true;
    private float move_x;


    void Update(){

        player_move();


    }

    //functions used for all the player actions
    void player_move(){

        //control
        move_x = Input.GetAxisRaw("Horizontal");

        //animation

        //player direction
        if(move_x > 0.0f && facing_right == false){

            flip_player();

        }
        else if(move_x < 0.0f && facing_right == true){

            flip_player();

        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (move_x * move_speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);


    }

    //function used to flip the character sprite alone the x axis
    void flip_player(){

        facing_right = !facing_right;
        Vector2 local_scale = gameObject.transform.localScale;
        local_scale.x *= -1;
        transform.localScale = local_scale;

    }

}

