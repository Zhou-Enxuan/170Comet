using UnityEngine;

public class Dialog : MonoBehaviour
{
    //能在unity里面看到
    public GameObject enterDialog;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            enterDialog.SetActive(true);
        }
    }
}
