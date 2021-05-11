using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButtonController : MonoBehaviour
{
    public Animator Anim;
    public int index;
    [SerializeField] GameObject Notes;

    void Start()
    {

    }

    void Update()
    {
        ButtonAnimation();
    }

    private void ButtonAnimation()
    {
        if(Input.GetKey("space"))
        {
            Anim.enabled = true;
            Anim.SetBool("press", true);
        }
        else{
            Anim.SetBool("press", false);
            if ((index < 5 && Input.GetKeyDown("down")) || (index < 5 && Input.GetKeyDown("s")))
            {
                ++index;
                
            }
            else if ((index > 1 && Input.GetKeyDown("up")) || (index > 1 && Input.GetKeyDown("w")))
            {
                --index;
            }
            transform.position = new Vector3(transform.position.x, Notes.transform.GetChild(index-1).gameObject.transform.position.y, 0);
        }
    }

}
