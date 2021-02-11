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

    private void FixedUpdate(){
        //碰撞到npcone的时候
        moveH = Input.GetAxisRaw("Horizontal");
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        moveV = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveH, moveV);
        //Debug.Log(Input.GetAxisRaw("Vertical"));
        rb.velocity = new Vector2(moveH, moveV) * moveSpeed * Time.deltaTime;

        if(moveH != 0 && moveV != 0)
        {
            if (direction.y == 1 && direction.x == -1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (direction.y == 1 && direction.x == 1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdWD_01");
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (direction.y == -1 && direction.x == -1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdSD_01");
                transform.localRotation = Quaternion.Euler(0, 180, 0);

            }

            if (direction.y == -1 && direction.x == 1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdSD_01");
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (direction.x == -1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (direction.x == 1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdD_01");
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }


            if (direction.y == 1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdW_01");
            }


            if (direction.y == -1)
            {
                sprite.sprite = Resources.Load<Sprite>("Level1/A_Level1BirdS_01");
            }
        }
    }

}
