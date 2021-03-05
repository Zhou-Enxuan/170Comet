using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdInDoorMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Vector2 direction;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 1.0f;
    public enum BirdsState { PEN, PAPER, NEWS, STATIC, FLOWER, GLASSES}
    public BirdsState currentState;
    private int Numdirection = 0;
    
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = BirdsState.STATIC;
        GameManager.instance.stopMoving = false;
        if(SceneManager.GetActiveScene().name == "")
        {
            currentState = BirdsState.NEWS;
        }
        else
        {
            currentState = BirdsState.STATIC;
        }

        if(GameManager.instance.isLv2Flower)
        {
            currentState = BirdsState.FLOWER;
        }
    }

    private void FixedUpdate(){
        //碰撞到npcone的时候
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;

            if (Numdirection == 0)
            {
                NDAnimation();
            }
            else if (Numdirection == 1)
            {
                NWDAnimation();
            }
            else if (Numdirection == 2)
            {
                WAnimation();
            }
            else if (Numdirection == 3)
            {
                SWAnimation();
            }
            else if (Numdirection == 4)
            {
                SDAnimation();
            }
            else if (Numdirection == 5)
            {
                SEDAnimation();
            }
            else if (Numdirection == 6)
            {
                EDAnimation();
            }
            else if (Numdirection == 7)
            {
                NEDAnimation();
            }
        }
        else
        {
            moveH = Input.GetAxisRaw("Horizontal");
            // Debug.Log(Input.GetAxisRaw("Horizontal"));
            moveV = Input.GetAxisRaw("Vertical");
            direction = new Vector2(moveH, moveV);
            // Debug.Log(Input.GetAxisRaw("Vertical"));
            rb.velocity = direction * moveSpeed;
            // Debug.Log(rb.velocity);
            if (moveH != 0 && moveV != 0)
            {
                if (direction.y == 1 && direction.x == -1)
                {
                    NWDAnimation();
                    Numdirection = 1;
                }

                if (direction.y == 1 && direction.x == 1)
                {
                    NEDAnimation();
                    Numdirection = 7;
                }

                if (direction.y == -1 && direction.x == -1)
                {
                    SWAnimation();
                    Numdirection = 3;
                }

                if (direction.y == -1 && direction.x == 1)
                {
                    SEDAnimation();
                    Numdirection = 5;
                }
            }
            else
            {
                if (direction.x == -1)
                {
                    WAnimation();
                    Numdirection = 2;
                }

                if (direction.x == 1)
                {
                    EDAnimation();
                    Numdirection = 6;
                }


                if (direction.y == 1)
                {
                    NDAnimation();
                    Numdirection = 0;
                }


                if (direction.y == -1)
                {
                    SDAnimation();
                    Numdirection = 4;
                }

                if(direction.y == 0 && direction.x == 0){
                    if(Numdirection == 0){
                        NDAnimation();
                    }else if(Numdirection == 1){
                        NWDAnimation();
                    }else if(Numdirection == 2){
                        WAnimation();
                    }else if(Numdirection == 3){
                        SWAnimation();
                    }else if(Numdirection == 4){
                        SDAnimation();
                    }else if(Numdirection == 5){
                        SEDAnimation();
                    }else if(Numdirection == 6){
                        EDAnimation();
                    }else if(Numdirection == 7){
                        NEDAnimation();
                    } 
                }
            }
        }
    }

    private void NDAnimation(){
        if (currentState == BirdsState.PEN) {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdW_Pen");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (currentState == BirdsState.PAPER) {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdW_Paper");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (currentState == BirdsState.FLOWER) {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdW_Flow");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdW_01");
        }
    }

    private void NWDAnimation(){
        if(currentState == BirdsState.PEN){
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdWD_Pen");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdWD_Paper");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdWD_Flow");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void WAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdD_Pen");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdD_Paper");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdD_Flow");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdS_01");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void SWAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdSD_Pen");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdSD_Paper");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdSD_Flow");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void SDAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdS_Pen");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdS_Paper");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdS_Flow");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdSD_01");
        }
    }

    private void SEDAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdSD_Pen");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdSD_Paper");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdSD_Flow");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void EDAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdD_Pen");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdD_Paper");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdD_Flow");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdS_01");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void NEDAnimation(){
        if(currentState == BirdsState.PEN)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdWD_Pen");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.PAPER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdWD_Paper");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(currentState == BirdsState.FLOWER)
        {
            sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdWD_Flow");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
