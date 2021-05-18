using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KingControl : MonoBehaviour
{
    private Animator kingAnim;
	public static GameObject nextHint;
    public static Text loseReasonText;
	private Transform target;
	private float turnWaitTime;
	private float wait;
	private bool isInvoke;

	//private float springTime = 1f;
	//private Vector2 newtarget;
	//private bool isCanSpring;
    private float throwTimer;
    private bool isStartThrow;
    public GameObject throwPos;
    public static GameObject throwItem;
    public int s1WinTimes;

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
    private bool isGirlFalled;

    public GameObject failUI;
    public GameObject Hint;

    [SerializeField] 
    public static bool isGameFailed;
    [SerializeField] 
	public static bool isToNextScene;
    
	public Transform[] movePos;
	public enum kingState {Stop, Throwing, Throwed, Breathing, Tracing, UnTracing, Spring, Stamping, FinalTracing};
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
        SoundManager.playBgm(7);
        kingAnim = this.GetComponent<Animator>();
    	nextHint = GameObject.Find("NextHint");
    	target = GameObject.Find("PlayerGirl").GetComponent<Transform>();
    	birdItem = GameObject.Find("Bird");
    	birdPos = birdItem.GetComponent<Transform>();
    	playerAnim = GameObject.Find("PlayerGirl").GetComponent<Animator>();
        loseReasonText = GameObject.Find("Reason").GetComponent<Text>();
        throwItem = GameObject.Find("DropItem");
    }

    void Start()
    {
        kingAnim.SetBool("isWalking", false);
        kingAnim.SetBool("isFaceR", false);
        kingAnim.SetBool("isThrow", false);
        kingAnim.SetBool("isStamping", false);
        isGameFailed = false;
        isToNextScene = false;
    	isInvoke = false;
    	curKingState = kingState.Stop;
    	turnWaitTime = 0.1f;
    	wait = turnWaitTime;
    	isKingFacePlayer = true; 
    	isMovingR = true; 
    	posNum = 0;
    	curNum = 0; //国王左右两点位置
    	//isCanSpring = false;
        isStartThrow = false;
        throwTimer = Random.Range(1, 2);
        throwPos.SetActive(false);
        throwItem.SetActive(false);

    	winningCount = 0;
    	sceneCount = 0;
    	countTimer = Random.Range(4, 6);
    	stampTime = 1.5f;
    	isShaked = true;
    	isCounting = false;
        isGirlFalled = false;
        isGirlRunning = false;
 		Dialog.PrintDialog("Lv4Part2Trace");
        failUI.SetActive(false);
        Hint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("sceneCount "+ sceneCount);
    	//Debug.Log("winningCount = " + winningCount);
    	//Debug.Log("loseCount = " + loseCount);
    	Debug.Log(curKingState);
    	// Debug.Log(isMovingR);
    	// Debug.Log(isKingFacePlayer);
        if (!isGameFailed) {
        	if (sceneCount == 0 && !GameManager.instance.IsDialogShow()) {
                kingAnim.SetBool("isWalking", true);
                kingAnim.SetBool("isFaceR", false);
        		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(36f, this.transform.position.y), speed * Time.deltaTime);
        		if (!birdItem.activeSelf) {
        			isToNextScene = true;
        			sceneSet(25.6f, 0, 37f, "KingStart", 2f);
        		// 	if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(25.6f, 0f, -10f)) {
    	    	// 		//Debug.Log("场景2");
    	    	// 		isToNextScene = false;
    	    	// 		//国王场景2初始位置
    	    	// 		if (!CheckTraceMoveState()) {
    	    	// 			turnWaitTime = 0;
    	    	// 			KingTurn();
        		// 		}
    	    	// 		this.transform.position = new Vector2(36.5f, this.transform.position.y);
    	    	// 		// curKingState = kingState.Stop;
     					// Invoke("KingStart", 2f);
    	    	// 		//Invoke("KingTrace", 2f);
    	    	// 	}
        		}
        	}
        	// 场景1
        	if (sceneCount == 1) {
        		if (winningCount >= s1WinTimes) {
        			if (winningCount == s1WinTimes) {
        				nextHint.SetActive(true);
                        Debug.Log("喘气");
                        curKingState = kingState.Breathing;
                        kingAnim.SetTrigger("Breath");
                    }
    	    		isToNextScene = true;
        			sceneSet(7.65f, 2, 19.5f, "KingTrace", 2f);
    	    		winningCount++;
    	    	}
        		KingThrowingMethod();
                KingUnTrancingMethod();
                // if (winningCount >= 2) {
                //     if (winningCount == 2) 
                //         nextHint.SetActive(true);
                //     isToNextScene = true;
                //     curKingState = kingState.UnTracing;
                //     sceneSet(7.65f, 2, 18.5f, "KingTrace", 2f);
                //     // if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(7.65f, 0f, -10f)) {
                //     //  //Debug.Log("场景2");
                //     //  curNum = 2; 
                //     //  isToNextScene = false;
                //     //  //国王场景2初始位置
                //     //  if (!CheckTraceMoveState()) {
                //     //      turnWaitTime = 0;
                //     //      KingTurn();
                //     //  }
                //     //  this.transform.position = new Vector2(18.5f, this.transform.position.y);
                //     //  curKingState = kingState.Stop;
                //     //  Invoke("KingTrace", 2f);
                //     // }
                //     winningCount++;
                // }
                // KingTracingMethod();
                // KingUnTrancingMethod();
                // KingSpringMethod();
        	} 
        	// 场景2
        	else if (sceneCount == 2) {
        		if (winningCount >= 2) {
                    if (!birdItem.activeSelf) {
            			if (winningCount == 2) {
            				nextHint.SetActive(true);                
                            kingAnim.SetBool("isStamping", false);
                            kingAnim.SetBool("isWalking", false);
                            Debug.Log("喘气");
                            curKingState = kingState.Breathing;
                            kingAnim.SetTrigger("Breath");
                        }
            			// curKingState = kingState.UnTracing;
            			isToNextScene = true;
            			countTimer = 0;
            			sceneSet(-9.85f, 4, 2f, "KingTrace", 3f);
                    }
                    else {
                        if (winningCount == 2) {    
                            nextHint.SetActive(true);             
                            kingAnim.SetBool("isStamping", false);
                            kingAnim.SetBool("isWalking", false);
                            Debug.Log("喘气");
                            curKingState = kingState.Breathing;
                            kingAnim.SetTrigger("Breath");
                        }
                    }
        			// if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(-9.85f, 0f, -10f)) {
    	    		// 	Debug.Log("场景3");
    	    		// 	curNum = 4; 
    	    		// 	isToNextScene = false;
    	    		// 	//国王场景2初始位置
    	    		// 	if (!CheckTraceMoveState()) {
    	    		// 		turnWaitTime = 0;
    	    		// 		KingTurn();
        			// 	}
    	    		// 	this.transform.position = new Vector2(1f, this.transform.position.y);
    	    		// 	curKingState = kingState.Stop;
    	    		// 	Invoke("KingTrace", 3f);
    	    		// }
    	    		winningCount++;
        		} 
                else {
        			isToNextScene = false;
        		
            		if (loseCount == 3){
            			Debug.Log("第二场景游戏失败");
                  	 	isGameFailed = true;
                        loseReasonText.text = "You were hurt by the jolt!";
                        GameFailed();
            		}
            		//随机国王跺脚
            		countTimer -= Time.deltaTime;
            		if (countTimer <= 0) {
            			curKingState = kingState.Stamping;
            		} 
            		else {
            			stampTime = 1.5f;
            			isShaked = true;
            			curKingState = kingState.UnTracing;
            		}
                }
                KingUnTrancingMethod();
                KingStampingMethod();
        	}
        	// 场景3
        	else if (sceneCount == 3) {
        		isToNextScene = true;
        		// Debug.Log("开始场景3");
    			this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(-18.85f, this.transform.position.y), 5f * Time.deltaTime);
    			
        	}
        } 
        else {
            //游戏失败 停止移动
            GameManager.instance.stopMoving = true;
            kingAnim.SetBool("isWalking", false);
            if (sceneCount == 1 && throwItem.activeSelf && DropingItem.isThrowed) {
                throwItem.SetActive(false);
                throwPos.SetActive(false);
            }
            if (GirlInGameMovement.isPlayFailUI) {
                GameFailed();
                GirlInGameMovement.isPlayFailUI = false;
            }
        }

        //游戏失败 转场
        if (failUI.activeSelf && Hint.activeSelf && Input.GetKeyDown(KeyCode.Space)) {
            LevelLoader.instance.LoadLevel("Level4Trace");
        }
    }

    void sceneSet(float camPos, int curnum, float kingPos, string method, float methodTime) {
    	if (!GameManager.instance.stopMoving && GameObject.Find("CM vcam1").GetComponent<Transform>().position == new Vector3(camPos, 0f, -10f)) {
			Debug.Log("新场景");
            // 场景3一开始
			if(sceneCount == 2)
				nextHint.SetActive(true);
			curNum = curnum; 
			isToNextScene = false;
			// 国王场景2初始位置
			if (!CheckTraceMoveState()) {
				turnWaitTime = 0;
				KingTurn();
			}
            kingAnim.SetBool("isWalking", false);
			this.transform.position = new Vector2(kingPos, this.transform.position.y);
			curKingState = kingState.Stop;
			if (!isInvoke) {
				Invoke(method, methodTime);
				isInvoke = true;
			}
		}

    }
    //扔权杖
    void KingThrowingMethod() {
        if (curKingState == kingState.Throwing) {
            if (!isStartThrow) {
                loseReasonText.text = "You Got Hit!";
                if (Vector2.Distance(this.transform.position, new Vector2(32.2f, this.transform.position.y)) < 0.1f) { 
                    this.transform.position = new Vector2(32.2f, this.transform.position.y);
                    kingAnim.SetBool("isWalking", false);
                    kingAnim.SetBool("isFaceR", false);
                    isStartThrow = true;
                }
                else {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(32.2f, this.transform.position.y), speed * Time.deltaTime);
                }
            }
            else {
                throwTimer -= Time.deltaTime;
                if (throwTimer <= 0) {
                    //扔权杖动画
                    //                     左，                        右
                    //var x = Random.Range(17.5f , 29f);
                    //throwPos.GetComponent<Transform>().position = new Vector2(x, throwPos.GetComponent<Transform>().position.y);
                    //throwPos.SetActive(true);
                    // throwItem.GetComponent<Transform>().position = new Vector2(x, throwItem.GetComponent<Transform>().position.y);
                    if (target.position.x <= 17.5f) {
                        //Debug.Log("位置17.5f");
                        throwPos.GetComponent<Transform>().position = new Vector2(17.5f, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(17.5f, throwItem.GetComponent<Transform>().position.y);
                    } 
                    else if (target.position.x >= 29f) {
                        //Debug.Log("位置29f");
                        throwPos.GetComponent<Transform>().position = new Vector2(29f, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(29f, throwItem.GetComponent<Transform>().position.y);
                    }
                    else {
                        Debug.Log("玩家位置");
                        throwPos.GetComponent<Transform>().position = new Vector2(target.position.x, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(target.position.x, throwItem.GetComponent<Transform>().position.y);
                    }
                    throwPos.SetActive(true);
                    kingAnim.SetBool("isThrow",true);
                    //Debug.Log("砸一下");
                    Invoke("KingDropItem",1.5f);
                    curKingState = kingState.Throwed;
                }
            }
        } 
        else if (curKingState == kingState.Throwed) {
            // Debug.Log("准备dropitem");
            if (throwItem.activeSelf && DropingItem.isThrowed) {
                throwItem.SetActive(false);
                throwPos.SetActive(false);
                kingAnim.SetBool("isThrow",false);
                throwTimer = Random.Range(1, 2);
                winningCount ++;
                curKingState = kingState.Throwing;
            } 
        }
    }
    //drop动画
    void KingDropItem() {
        throwItem.SetActive(true);
    }
    //king动画
    void KingThrowed() {
        kingAnim.SetBool("isThrow",false);
    }
    //喘气动画
    void KingBreathed() {
        kingAnim.SetBool("isWalking", true);
        curKingState = kingState.UnTracing;
    }

    //跺脚
    void KingStampingMethod() {
    	if (curKingState == kingState.Stamping) {
    		 Debug.Log("Stamping");
    		if (isShaked) {
                kingAnim.SetBool("isWalking", false);
                kingAnim.SetBool("isStamping",true);
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
            //女孩没动
                if (!isCounting){
                    Debug.Log("没动");
                    winningCount ++;
                } else {
                    isCounting = false;
                }
                kingAnim.SetBool("isWalking", true);
                kingAnim.SetBool("isStamping",false);
    			countTimer = Random.Range(6,8);
    		}
    		else {
    			//女孩在动
    			if (isGirlRunning && !isCounting) {
    				/////////////////
    				//跺脚animation
                    GameManager.instance.stopMoving = true;
    				Debug.Log("跺脚animation + 女孩跌倒animation");
    				/////////////////
    				//小鸟没有掉出，找位置
    				if(!birdItem.activeSelf) {
						if (!playerAnim.GetBool("FaceR") && target.position.x <= movePos[curNum+1].position.x + 3f) {
							//女孩到左边尽头，转身，小鸟要朝右边掉
							playerAnim.SetBool("FaceR", true);
                            playerAnim.SetBool("Falling", true);
							birdPos.position =  new Vector2(target.position.x + 3.5f, birdPos.position.y);
							//birdItem.SetActive(true);
						}
						else if (!playerAnim.GetBool("FaceR") && target.position.x > movePos[curNum+1].position.x + 3f) {
							playerAnim.SetBool("Falling", true);
                            birdPos.position =  new Vector2(target.position.x - 3.5f, birdPos.position.y);
							//birdItem.SetActive(true);
						}
						else if(playerAnim.GetBool("FaceR") && target.position.x >= movePos[curNum].position.x - 3f) {
							playerAnim.SetBool("FaceR", false);
                            playerAnim.SetBool("Falling", true);
							birdPos.position =  new Vector2(target.position.x - 3.5f, birdPos.position.y);
							//birdItem.SetActive(true);
						}
						else if (playerAnim.GetBool("FaceR") && target.position.x < movePos[curNum].position.x - 3f) {
                            playerAnim.SetBool("Falling", true);
							birdPos.position =  new Vector2(target.position.x + 3.5f, birdPos.position.y);
							//birdItem.SetActive(true);
						}
    				} 
                    else {
                        if (!playerAnim.GetBool("FaceR") && target.position.x <= movePos[curNum+1].position.x + 3f) {
                            //女孩到左边尽头，转身，小鸟要朝右边掉
                            playerAnim.SetBool("FaceR", true);
                            playerAnim.SetBool("Falling", true);
                        }
                        else if (!playerAnim.GetBool("FaceR") && target.position.x > movePos[curNum+1].position.x + 3f) {
                            playerAnim.SetBool("Falling", true);
                        }
                        else if(playerAnim.GetBool("FaceR") && target.position.x >= movePos[curNum].position.x - 3f) {
                            playerAnim.SetBool("FaceR", false);
                            playerAnim.SetBool("Falling", true);
                        }
                        else if (playerAnim.GetBool("FaceR") && target.position.x < movePos[curNum].position.x - 3f) {
                            playerAnim.SetBool("Falling", true);
                        }
                    }
    				loseCount ++;
    				isCounting = true;
    			}
    		}
    	}
    }
    
    //来回走
    void KingUnTrancingMethod() {
    	if (curKingState == kingState.UnTracing) {
            // 女孩站起来
            if(playerAnim.GetBool("Falling")) {
                playerAnim.SetBool("Falling", false);
            } 
    		//Debug.Log("UnTracing");
    		// if (!isToNextScene && sceneCount == 1 && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    		// 	curKingState = kingState.Tracing;
    		// }
    		// else 
    		// {
	    		if (isMovingR) {
	    			posNum = curNum + 1;
	    		} else {
	    			posNum = curNum;
	    		}

	    		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y), speed * Time.deltaTime);
		        if (Vector2.Distance(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y)) < 0.1f) {
		        	KingTurn();
			    }
    		//}	
    	}
    }


    bool CheckTraceMoveState() {
        Debug.Log("CheckTraceMoveState");
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
    //转场设数据
        Debug.Log("kingtrace重置");
        kingAnim.SetBool("isWalking", true);
    	if (sceneCount == 1) {
    		if (winningCount >= 3) {
    			sceneCount = 2;
	    		winningCount = 0;
    		} 
    	} 
    	else if (sceneCount == 2 && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding && winningCount >=2) {
    		// Debug.Log("开始");
    		sceneCount = 3;
    		curKingState = kingState.FinalTracing;
    	}
    	else if (sceneCount == 0) {
    		sceneCount = 1;
    		curKingState = kingState.Throwing;
        }
    	else {
    		curKingState = kingState.UnTracing;
    	}
    	//重置invoke
    	isInvoke = false;
    }

    void KingTurn() {
    	Debug.Log("转身");
		if (turnWaitTime > 0) {
	        turnWaitTime -= Time.deltaTime;
    	} 
    	else {
    		//正在向右走，需要转身
     		if (isMovingR) {
                kingAnim.SetBool("isFaceR", true);
    			//this.transform.eulerAngles = new Vector3 (0, -180, 0);
         		isMovingR = false;
        	} 
        	//正在向左走，需要转身
          	else {
                kingAnim.SetBool("isFaceR", false);
				//this.transform.eulerAngles = new Vector3 (0, 0, 0);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding)
        {
            Debug.Log("游戏失败");
            playerAnim.SetTrigger("Losed");
            loseReasonText.text = "The King caught you!";
           // SceneManager.LoadScene 
            isGameFailed = true;
        }
        if (collision.gameObject.name == "Bird") {
            Debug.Log("碰小鸟");
            //国王提起小鸟动画
            //playerAnim.SetTrigger("Losed");
            loseReasonText.text = "The King caught your friend!";
            GameFailed();
           // SceneManager.LoadScene 
            isGameFailed = true;
        }
	}

	void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void GameFailed() {
        failUI.SetActive(true);     
        Invoke("EndHint",0.7f);
    }

    void EndHint() {
        Hint.SetActive(true);
    }

    // //追踪
    // void KingTracingMethod() {
    //  if (curKingState == kingState.Tracing) {
    //      Debug.Log("Tracing");
    //      //判断king面向左or右
    //      if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding) {
    //          if (!CheckTraceMoveState()) {
    //              //turnWaitTime = 0;
    //              KingTurn();
    //          }
    //          //follow
          //    if (Vector2.Distance(this.transform.position, new Vector2(target.position.x, this.transform.position.y)) > 5) {
    //              //Debug.Log("追踪");
    //              this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(target.position.x, this.transform.position.y), speed * Time.deltaTime);
          //    } 
          //    else {
          //        curKingState = kingState.Spring;
          //    }
    //      }
    //      else if (GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.Hiding) {
    //          curKingState = kingState.UnTracing;
    //      } 
            
    //  }
    // }

  //   void KingSpringPos() {
  //    if (!isMovingR) {
        //  //向右冲
        //  newtarget = new Vector2(this.transform.position.x + 10, this.transform.position.y);
        //  if (newtarget.x >= movePos[curNum].position.x) {
        //      newtarget.x = movePos[curNum].position.x;
        //  } 
        //  //Debug.Log("111");
        // } 
        // else {
        //  //向左冲
        //  newtarget = new Vector2(this.transform.position.x - 10, this.transform.position.y);
        //  if (newtarget.x <= movePos[curNum+1].position.x) {
        //      newtarget.x = movePos[curNum+1].position.x;
        //  } 
        //  //Debug.Log("222");
        // }
  //   }

    // //冲刺
    // void KingSpringMethod() { 
    //  if (curKingState == kingState.Spring) {
    //      Debug.Log("Spring");
          //   if (springTime <= 1f) {
          //    isCanSpring = false;
          //    springTime -= Time.deltaTime;
          //    if (springTime <= 0) {
          //        isCanSpring = true;
          //    }
          //   }

       //   if (!isCanSpring) {
          //    if (!CheckTraceMoveState()) {
          //        //turnWaitTime = 0;
          //        KingTurn();
    //              KingSpringPos();
                // } 
                // else {
                //  KingSpringPos();
                // }
          //   }
          //   else {
                // if (Vector2.Distance(this.transform.position, newtarget) > 1f) {
             //         this.transform.position = Vector2.MoveTowards(this.transform.position, newtarget, 5f * Time.deltaTime);
             //         // Debug.Log("CHONGCI");
             //    } 
             //    else if (curKingState == kingState.Spring) {
             //     winningCount ++;
             //     curKingState = kingState.UnTracing;
             //     springTime = 1f;
             //     }
          //   }
    //  }
    // }
}
