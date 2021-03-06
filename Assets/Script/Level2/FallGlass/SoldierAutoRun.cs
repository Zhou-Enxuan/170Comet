using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAutoRun : MonoBehaviour
{
	public float speed;
	public static float waitTime = 0.1f;
	public Transform[] movePos;
	public GameObject soldier01;
	public GameObject soldier02;
	private int i;
	private bool moveingRight;
	private float wait;
	private Animator Soldier01Anim;
	private Animator Soldier02Anim;

    // Start is called before the first frame update
    void Start()
    {
    	i = 1;
    	moveingRight = false;
    	wait = waitTime;
    	Soldier01Anim = soldier01.GetComponent<Animator>();
    	Soldier02Anim = soldier02.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (CaptainAction.isSoldierRun) {
	        this.transform.position = Vector2.MoveTowards(this.transform.position, movePos[i].position, speed * Time.deltaTime);
	        Soldier01Anim.SetBool("isWalking",true);
	        Soldier02Anim.SetBool("isWalking",true);
	        if (Vector2.Distance(this.transform.position, this.movePos[i].position) < 0.1f) {
	        	if (waitTime > 0) {
	        		waitTime -= Time.deltaTime;
	        	} 
	        	else {
		        	if (moveingRight) {
		        		Soldier01Anim.SetBool("isOnBack",false);
		        		Soldier02Anim.SetBool("isOnBack",false);
		        		//this.transform.eulerAngles = new Vector3 (0, -180, 0);
		        		moveingRight = false;
		        	} 
		        	else {
		        		Soldier01Anim.SetBool("isOnBack",true);
		        		Soldier02Anim.SetBool("isOnBack",true);
						//this.transform.eulerAngles = new Vector3 (0, 0, 0);
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
		// 追玩家
		else if (CaptainAction.isSoldierTrace) {
			// this.transform.eulerAngles = new Vector3 (0, -180, 0);
			this.transform.position = Vector2.MoveTowards(this.transform.position, GameObject.Find("Player").GetComponent<Transform>().position, speed * Time.deltaTime);
		}
		// 静止
		else {
			Soldier01Anim.SetBool("isWalking",false);
			Soldier02Anim.SetBool("isWalking",false);
		}
    }
}
