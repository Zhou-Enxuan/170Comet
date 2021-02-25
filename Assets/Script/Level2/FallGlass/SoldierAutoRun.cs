using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAutoRun : MonoBehaviour
{
	public float speed = 3f;
	public static float waitTime = 0.1f;
	public Transform[] movePos;

	private int i = 1;
	private bool moveingRight = true;
	private float wait;

    // Start is called before the first frame update
    void Start()
    {
       wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
    	if (CaptainAction.isSoldierRun) {
	        this.transform.position = Vector2.MoveTowards(this.transform.position, movePos[i].position, speed * Time.deltaTime);
	        if (Vector2.Distance(this.transform.position, this.movePos[i].position) < 0.1f) {
	        	if (waitTime > 0) {
	        		waitTime -= Time.deltaTime;
	        	} 
	        	else {
		        	if (moveingRight) {
		        		this.transform.eulerAngles = new Vector3 (0, -180, 0);
		        		moveingRight = false;
		        	} 
		        	else {
						this.transform.eulerAngles = new Vector3 (0, 0, 0);
		        		moveingRight = true;
		        	}
		        	if (i == 0) {
		        		i = 1;
		        	}
		        	else {
		        		i = 0;
		        	}
		        	waitTime = wait;
		        }
	        }
		}
		else if (CaptainAction.isSoldierTrace) {
			this.transform.eulerAngles = new Vector3 (0, -180, 0);
			this.transform.position = Vector2.MoveTowards(this.transform.position, GameObject.Find("Player").GetComponent<Transform>().position, speed * Time.deltaTime);
		}
    }
}
