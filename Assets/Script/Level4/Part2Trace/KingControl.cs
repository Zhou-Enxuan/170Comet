using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingControl : MonoBehaviour
{
	private Transform target;
	private float turnWaitTime;
	private float wait;
	private Vector2 newtarget;
	private bool isSpring = false;

	public Transform[] movePos;
	public enum kingState {Stop, Tracing, UnTracing, Spring, Sprung, Stamping};
    public static kingState curKingState;

	[SerializeField] 
	private float speed = 2f;
	private int posNum;
	[SerializeField] 
	private bool isMovingR;//正在向右走，需要转身
	[SerializeField] 
	private bool isKingFacePlayer; //面向玩家

    // Start is called before the first frame update
    void Awake() {
    	target = GameObject.Find("PlayerGirl").GetComponent<Transform>();
    	curKingState = kingState.Stop;
    	turnWaitTime = 0.1f;
    	wait = turnWaitTime;
    	isKingFacePlayer = true;
    	isMovingR = true;
    }

    void Start()
    {
 		//玩家进入场景2秒后
 		Invoke("KingStart", 2f);
    }

    // Update is called once per frame
    void Update()
    {
    	Debug.Log(curKingState);
    	// Debug.Log(isMovingR);
    	// Debug.Log(isKingFacePlayer);
    	//追踪
    	if (curKingState == kingState.Tracing) {
    		//判断king面向左or右
    		if (GirlInGameMovement.isGirlHiding && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    			if (!CheckTraceMoveState())
    				KingTurn();
    		}
    		else if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.Hiding) {
    			curKingState = kingState.UnTracing;
    		} 
    		
    		//follow
	    	if (Vector2.Distance(this.transform.position, target.position) > 5) {
	        	this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
	    	} else {
	    		curKingState = kingState.Spring;
	    	}
		    
    	}
    	//来回走
    	else if (curKingState == kingState.UnTracing) {
    		isSpring = false;
    		if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    			curKingState = kingState.Tracing;
    		}
    		else {
	    		if (isMovingR) {
	    			posNum = 0;
	    		} else {
	    			posNum = 1;
	    		}

	    		this.transform.position = Vector2.MoveTowards(this.transform.position, movePos[posNum].position, speed * Time.deltaTime);
		        if (Vector2.Distance(this.transform.position, this.movePos[posNum].position) < 0.1f) {
		        	KingTurn();
			    }
    		}	
    	}
    	//冲刺 
    	else if (curKingState == kingState.Spring) {
    		// Debug.Log(newtarget);
	    	if (!isSpring) {
		    	if (!CheckTraceMoveState()) {
		    		turnWaitTime = 0;
		    		KingTurn();
    				if (!isMovingR) {
			    		//向右冲
						newtarget = new Vector2(this.transform.position.x + 10, this.transform.position.y);
						if (newtarget.x >= movePos[1].position.x) {
							newtarget = movePos[1].position;
						} 
						//Debug.Log("111");
			    	} 
			    	else {
			    		//向左冲
			    		newtarget = new Vector2(this.transform.position.x - 10, this.transform.position.y);
			    		if (newtarget.x <= movePos[0].position.x) {
							newtarget = movePos[0].position;
						} 
						//Debug.Log("222");
			    	}
				} 
				else {
					if (!isMovingR) {
			    		//向右冲
						newtarget = new Vector2(this.transform.position.x + 10, this.transform.position.y);
						if (newtarget.x >= movePos[1].position.x) {
							newtarget = movePos[1].position;
						} 
						//Debug.Log("3333");
			    	} 
			    	else {
			    		//向左冲
			    		newtarget = new Vector2(this.transform.position.x - 10, this.transform.position.y);
			    		if (newtarget.x <= movePos[0].position.x) {
							newtarget = movePos[0].position;
						} 
						//Debug.Log("444");
			    	}
				}
		    	isSpring = true;
		    }
	    		Invoke("KingSpring",1f);
    	}
    }

    bool CheckTraceMoveState() {
    	if (this.transform.position.x > target.position.x && isMovingR) {
    		isKingFacePlayer = true;
    	} 
    	else if (this.transform.position.x > target.position.x && !isMovingR) {
    		isKingFacePlayer = false;
    	}
    	else if (this.transform.position.x < target.position.x && !isMovingR) {
    		isKingFacePlayer = true;
    	}
    	else if (this.transform.position.x < target.position.x && isMovingR) {
    		isKingFacePlayer = false;
    	}
    	return isKingFacePlayer;
    }

    void KingStart() {
        CameraShake.ShakeCamera(3f,1f);
        Invoke("KingTrace", 1f);
    }

    void KingTrace() {
    	if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    		curKingState = kingState.Tracing;
    	} 
    	else {
    		curKingState = kingState.UnTracing;
    	}
    }

    void KingTurn() {
		if (turnWaitTime > 0) {
	        turnWaitTime -= Time.deltaTime;
    	} 
    	else {
    		//正在向右走，需要转身
     		if (isMovingR) {
    //     		Soldier01Anim.SetBool("isOnBack",false);
    //     		Soldier02Anim.SetBool("isOnBack",false);
    			this.transform.eulerAngles = new Vector3 (0, -180, 0);
         		isMovingR = false;
        	} 
        	//正在向左走，需要转身
          	else {
    //     		Soldier01Anim.SetBool("isOnBack",true);
    //     		Soldier02Anim.SetBool("isOnBack",true);
				this.transform.eulerAngles = new Vector3 (0, 0, 0);
          		isMovingR = true;
          	}
        	// if (posNum == 0) {
        	// 	posNum = 1;
        	// }
        	// else {
        	// 	posNum = 0;
        	// }
        	turnWaitTime = wait;
        	if (curKingState == kingState.Tracing) 
				GirlInGameMovement.isGirlHiding = false;
        }
    }

    void KingSpring() {
    	if (Vector2.Distance(this.transform.position, newtarget) > 1f) {
	    		this.transform.position = Vector2.MoveTowards(this.transform.position, newtarget, 5f * Time.deltaTime);
	    		// Debug.Log("CHONGCI");
	    } 
	    else {
	    		// this.transform.position = newtarget;
	    		curKingState = kingState.UnTracing;
	   	}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding)
        {
            Debug.Log("游戏失败");
        }
	}

	void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
