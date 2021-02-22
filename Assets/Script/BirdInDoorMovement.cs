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
    
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.instance.stopMoving = false;
    }

    private void FixedUpdate(){
        //碰撞到npcone的时候
        if (GameManager.instance.stopMoving)
        {
            rb.velocity = Vector2.zero;
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
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdWD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdWD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdWD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                }

                if (direction.y == 1 && direction.x == 1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdWD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdWD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdWD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }

                if (direction.y == -1 && direction.x == -1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdSD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdSD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdSD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdSD_01");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                }

                if (direction.y == -1 && direction.x == 1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdSD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdSD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdSD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdSD_01");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            else
            {
                if (direction.x == -1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
                        transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                }

                if (direction.x == 1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdD_Pen");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdD_Paper");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdD_Flow");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }


                if (direction.y == 1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdW_Pen");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdW_Paper");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdW_Flow");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdW_01");
                    }
                }


                if (direction.y == -1)
                {
                    if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Pen == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Pen/A_Level1BirdS_Pen");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv1Paper == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Paper/A_Level1BirdS_Paper");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else if(GameObject.Find("GameManager").GetComponent<GameManager>().isLv2Flower == true){
                        sprite.sprite = Resources.Load<Sprite>("Level1/Bird_Flow/A_Level1BirdS_Flow");
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }else{
                        sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdS_01");
                    }
                }
            }
        }
    }

}
