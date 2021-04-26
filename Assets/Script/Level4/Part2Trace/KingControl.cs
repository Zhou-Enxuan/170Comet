using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingControl : MonoBehaviour
{
	private Transform target;
	private float turnWaitTime;
	private float wait;
	private float springTime = 1f;
	private Vector2 newtarget;
	private bool isCanSpring;
	private int posNum;
	private int curNum;

	private float countTimer;
	private float stampTime;
	public static GameObject birdItem;
	private Transform birdPos;
	private Animator playerAnim;
	private bool isCounting;
	private bool isShaked;
	private bool isGirlRunning;

	public static bool isToNextScene = false;
	public Transform[] movePos;
	public enum kingState {Stop, Tracing, UnTracing, Spring, Stamping, FinalTracing};
    public static kingState curKingState;

	public static int sceneCount;//场景1，2，3
	[SerializeField] 
	private float speed = 2f;
	[SerializeField] 
	private bool isMovingR;//正在向右走，需要转身
	[SerializeField] 
	private bool isKingFacePlayer; //面向玩家
	[SerializeField] 
	private int winningCount;
	private int loseCount;

    // Start is called before the first frame update
    void Awake() {
    	target = GameObject.Find("PlayerGirl").GetComponent<Transform>();
    	curKingState = kingState.Stop;
    	turnWaitTime = 0.1f;
    	wait = turnWaitTime;
    	isKingFacePlayer = true; 
    	isMovingR = true; 
    	posNum = 0;
    	curNum = 0; //国王左右两点位置
    	isCanSpring = false;
    	winningCount = 0;
    	sceneCount = 0;
    	countTimer = Random.Range(4, 6);
    	stampTime = 2f;
    	birdItem = GameObject.Find("Bird");
    	birdPos = birdItem.GetComponent<Transform>();
    	playerAnim = GameObject.Find("PlayerGirl").GetComponent<Animator>();
    	birdItem.SetActive(false);
    	isShaked = true;
    	isCounting = false;
    }

    void Start()
    {
 		//玩家进入场景2秒后
 		Invoke("KingStart", 2f);
    }

    // Update is called once per frame
    void Update()
    {
    	Debug.Log("winningCount = " + winningCount);
    	Debug.Log("loseCount = " + loseCount);
    	//Debug.Log(curKingState);
    	// Debug.Log(isMovingR);
    	// Debug.Log(isKingFacePlayer);
    	// 场景1
    	if (sceneCount == 0) {
    		if (winningCount == 2) {
	    		isToNextScene = true;
	    		curKingState = kingState.UnTracing;
	    		if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(7.65f, 0f, -10f)) {
	    			//Debug.Log("场景2");
	    			curNum = 2; 
	    			isToNextScene = false;
	    			//国王场景2初始位置
	    			if (!CheckTraceMoveState()) {
	    				turnWaitTime = 0;
	    				KingTurn();
    				}
	    			this.transform.position = new Vector2(18.5f, this.transform.position.y);
	    			curKingState = kingState.Stop;
	    			Invoke("KingTrace", 2f);
	    		}
	    	}
    		KingTracingMethod();
    		KingUnTrancingMethod();
    		KingSpringMethod();
    	} 
    	// 场景2
    	else if (sceneCount == 1) {
    		if (winningCount >= 2 && !birdItem.activeSelf) {
    			curKingState = kingState.UnTracing;
    			isToNextScene = true;
    			countTimer = 0;
    			if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(-9.85f, 0f, -10f)) {
	    			Debug.Log("场景3");
	    			curNum = 4; 
	    			isToNextScene = false;
	    			//国王场景2初始位置
	    			if (!CheckTraceMoveState()) {
	    				turnWaitTime = 0;
	    				KingTurn();
    				}
	    			this.transform.position = new Vector2(1f, this.transform.position.y);
	    			curKingState = kingState.Stop;
	    			Invoke("KingTrace", 3f);
	    		}
    		} else {
    			isToNextScene = false;
    		}
    		if (loseCount == 3){
    			Debug.Log("第二场景游戏失败");
    		}
    		KingUnTrancingMethod();
    		KingStampingMethod();
    		//随机国王跺脚
    		countTimer -= Time.deltaTime;
    		if (countTimer <= 0) {
    			curKingState = kingState.Stamping;
    		} 
    		else {
    			stampTime = 2f;
    			isShaked = true;
    			curKingState = kingState.UnTracing;
    		}
    	}
    	// 场景3
    	else if (sceneCount == 2) {
    		isToNextScene = true;
    		Debug.Log("开始场景3");
			this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(-18.85f, this.transform.position.y), 5f * Time.deltaTime);
			
    	}
    }

    //跺脚
    void KingStampingMethod() {
    	if (curKingState == kingState.Stamping) {
    		Debug.Log("Stamping");
    		if (isShaked) {
	    		CameraShake.ShakeCamera(3f,1f);
	    		isShaked = false;
    		}
    		//stamp时小女孩是否在动
    		if (playerAnim.GetFloat("Speed") != 0) {
	    			isGirlRunning = true;
	    		} else {
	    			isGirlRunning = false;
	    	}
	    	//1秒跺脚
    		stampTime -= Time.deltaTime;
    		if (stampTime <= 0) {
    			isCounting = false;
    			GameManager.instance.stopMoving = false;
    			countTimer = Random.Range(4, 6);
    		}
    		else {
    			//女孩在动
    			if (isGirlRunning && !isCounting) {
    				/////////////////
    				//跺脚animation
    				// playerAnim.SetBool();
    				//Debug.Log("跺脚animation + 女孩跌倒animation");
    				/////////////////
    				//小鸟没有掉出，找位置
    				if(!birdItem.activeSelf) {
						if (!playerAnim.GetBool("FaceR") && target.position.x <= movePos[curNum+1].position.x + 1f) {
							//女孩到左边尽头，转身，小鸟要朝右边掉
							playerAnim.SetBool("FaceR", true);
							birdPos.position =  new Vector2(target.position.x + 1.5f, birdPos.position.y);
							birdItem.SetActive(true);
						}
						else if (!playerAnim.GetBool("FaceR") && target.position.x > movePos[curNum+1].position.x + 1f) {
							birdPos.position =  new Vector2(target.position.x - 1.5f, birdPos.position.y);
							birdItem.SetActive(true);
						}
						else if(playerAnim.GetBool("FaceR") && target.position.x >= movePos[curNum].position.x - 1f) {
							playerAnim.SetBool("FaceR", false);
							birdPos.position =  new Vector2(target.position.x - 1.5f, birdPos.position.y);
							birdItem.SetActive(true);
						}
						else if (playerAnim.GetBool("FaceR") && target.position.x < movePos[curNum].position.x - 1f) {
							birdPos.position =  new Vector2(target.position.x + 1.5f, birdPos.position.y);
							birdItem.SetActive(true);
						}
    				}
    				loseCount ++;
    				GameManager.instance.stopMoving = true;
    				isCounting = true;
    			}
    			//女孩没动
    			else if (!isGirlRunning && !isCounting){
    				Debug.Log("没动");
    				winningCount ++;
    				isCounting = true;
    			}
    		}
    	}

    }

    //追踪
    void KingTracingMethod() {
    	if (curKingState == kingState.Tracing) {
    		Debug.Log("Tracing");
    		//判断king面向左or右
    		if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    			if (!CheckTraceMoveState()) {
    				//turnWaitTime = 0;
    				KingTurn();
    			}
    			//follow
		    	if (Vector2.Distance(this.transform.position, new Vector2(target.position.x, this.transform.position.y)) > 5) {
    				//Debug.Log("追踪");
    				this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(target.position.x, this.transform.position.y), speed * Time.deltaTime);
		    	} 
		    	else {
		    		curKingState = kingState.Spring;
		    	}
    		}
    		else if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.Hiding) {
    			curKingState = kingState.UnTracing;
    		} 
		    
    	}
    }

    //来回走
    void KingUnTrancingMethod() {
    	if (curKingState == kingState.UnTracing) {
    		Debug.Log("UnTracing");
    		if (!isToNextScene && sceneCount == 0 && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    			curKingState = kingState.Tracing;
    		}
    		else 
    		{
	    		if (isMovingR) {
	    			posNum = curNum + 1;
	    		} else {
	    			posNum = curNum;
	    		}

	    		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y), speed * Time.deltaTime);
		        if (Vector2.Distance(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y)) < 0.1f) {
		        	KingTurn();
			    }
    		}	
    	}
    }

    //冲刺
    void KingSpringMethod() { 
    	if (curKingState == kingState.Spring) {
    		Debug.Log("Spring");
		    if (springTime <= 1f) {
		    	isCanSpring = false;
		    	springTime -= Time.deltaTime;
		    	if (springTime <= 0) {
		    		isCanSpring = true;
		    	}
		    }

	    	if (!isCanSpring) {
		    	if (!CheckTraceMoveState()) {
		    		//turnWaitTime = 0;
		    		KingTurn();
    				KingSpringPos();
				} 
				else {
					KingSpringPos();
				}
		    }
		    else {
				if (Vector2.Distance(this.transform.position, newtarget) > 1f) {
			    		this.transform.position = Vector2.MoveTowards(this.transform.position, newtarget, 5f * Time.deltaTime);
			    		// Debug.Log("CHONGCI");
			    } 
			    else if (curKingState == kingState.Spring) {
			    	winningCount ++;
			    	curKingState = kingState.UnTracing;
			    	springTime = 1f;
			   	}
		    }
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
    	if (sceneCount == 0 && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    		if (winningCount == 2) {
    			sceneCount = 1;
	    		winningCount = 0;
	    		curKingState = kingState.UnTracing;
    		} 
    		else {
    			curKingState = kingState.Tracing;
    		}
    	} 
    	else if (sceneCount == 1 && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding && winningCount >=2) {
    	Debug.Log("开始");
    		sceneCount = 2;
    		curKingState = kingState.FinalTracing;
    	}
    	else {
    		curKingState = kingState.UnTracing;
    	}
    }

    void KingTurn() {
    	//Debug.Log("转身");
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
    //     	if (curKingState == kingState.Tracing) {
				// GirlInGameMovement.isGirlHiding = false;
    //     	}
        }
    }

    void KingSpringPos() {
    	if (!isMovingR) {
			//向右冲
			newtarget = new Vector2(this.transform.position.x + 10, this.transform.position.y);
			if (newtarget.x >= movePos[curNum].position.x) {
				newtarget.x = movePos[curNum].position.x;
			} 
			//Debug.Log("111");
		} 
		else {
			//向左冲
			newtarget = new Vector2(this.transform.position.x - 10, this.transform.position.y);
			if (newtarget.x <= movePos[curNum+1].position.x) {
				newtarget.x = movePos[curNum+1].position.x;
			} 
			//Debug.Log("222");
		}
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.name == "Bird") && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding)
        {
           Debug.Log("游戏失败");
        }
	}

	void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
