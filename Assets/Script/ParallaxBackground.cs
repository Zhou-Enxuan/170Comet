using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Vector2 parallax_effect_multiplier;
    public bool infinite_horizontal;
    public bool infinite_vertical;

    private Transform camera_transform;
    private Vector3 last_camera_position;
    private float texture_unit_size_x;
    private float texture_unit_size_y;

    private void Start()
    {

        camera_transform = Camera.main.transform;
        last_camera_position = camera_transform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        texture_unit_size_x = texture.width / sprite.pixelsPerUnit;
        texture_unit_size_y = texture.height / sprite.pixelsPerUnit;

    }

    // Update is called once per frame
    private void LateUpdate()
    {

        Vector3 delta_movement = camera_transform.position - last_camera_position;
        transform.position += new Vector3(delta_movement.x * parallax_effect_multiplier.x, 
                                            delta_movement.y * parallax_effect_multiplier.y);
        last_camera_position = camera_transform.position;

        if(infinite_horizontal){
            if(Mathf.Abs(camera_transform.position.x - transform.position.x) >= texture_unit_size_x){

                float offset_position_x = (camera_transform.position.x - transform.position.x) % texture_unit_size_x;
                transform.position = new Vector3(camera_transform.position.x + offset_position_x, transform.position.y);

            }
        }

        if(infinite_vertical){
            if(Mathf.Abs(camera_transform.position.y - transform.position.y) >= texture_unit_size_y){

                float offset_position_y = (camera_transform.position.y - transform.position.y) % texture_unit_size_y;
                transform.position = new Vector3(transform.position.x, camera_transform.position.y + offset_position_y);

            }
        }

    }
}
