using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject enterDialog;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            enterDialog.SetActive(true);
        }
    }
}
