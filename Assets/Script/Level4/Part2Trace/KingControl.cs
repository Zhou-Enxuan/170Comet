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
    private bool isSEplayed;

    private int posNum;
    private int curNum;
    // 扔权杖
    private float throwTimer;
    private bool isStartThrow;
    public static GameObject throwPos;
    public static GameObject throwPos01;
    public static GameObject throwPos02;
    public static GameObject throwItem;
    public static GameObject throwItem01;
    public static GameObject throwItem02;
    public int s1WinTimes;
    private float dropPos;
    // 跺脚
	public static GameObject birdItem;
	private Transform birdPos;
	private Animator playerAnim;
	private bool isShaked; //镜头动
    private bool isStomp;
    private bool isEndstomp;

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
        throwItem01 = GameObject.Find("DropItem01");
        throwItem02 = GameObject.Find("DropItem02");
        throwPos = GameObject.Find("DropPos");
        throwPos01 = GameObject.Find("DropPos01");
        throwPos02 = GameObject.Find("DropPos02");
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
        isSEplayed = false;
    	curKingState = kingState.Stop;
    	turnWaitTime = 0.1f;
    	wait = turnWaitTime;
    	isKingFacePlayer = true; 
    	isMovingR = true; 
    	posNum = 0;
    	curNum = 0; //国王左右两点位置
        isStartThrow = false;
        throwTimer = Random.Range(1, 2);
        throwPos.SetActive(false);
        throwPos01.SetActive(false);
        throwPos02.SetActive(false);
        throwItem.SetActive(false);
        throwItem01.SetActive(false);
        throwItem02.SetActive(false);

    	winningCount = 0;
    	sceneCount = 0;
    	isShaked = true;
        isStomp = false;
        isEndstomp = false;

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
        	} 
        	// 场景2
        	else if (sceneCount == 2) {
        		if (winningCount >= 2) {
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
                    // countTimer = 0;
                    sceneSet(-9.85f, 4, 2f, "KingTrace", 3f);
    	    		winningCount++;
        		} 
                else {
        			isToNextScene = false;

                    if (curKingState == kingState.Stop) {
                        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(14f, this.transform.position.y), speed * Time.deltaTime);
                        if (Vector2.Distance(this.transform.position, new Vector2(14f, this.transform.position.y)) < 0.1f) { 
                            Dialog.PrintDialog("Lv4Part2Trace02");
                            kingAnim.SetBool("isWalking", false);
                            curKingState = kingState.Stamping; 
                        }
                    }
                }
                KingTracingMethod();
                KingStompMethod();
                KingUnTrancingMethod();
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
                throwItem01.SetActive(false);
                throwPos01.SetActive(false);
                throwItem02.SetActive(false);
                throwPos02.SetActive(false);
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
    //
    void KingTracingMethod() {
        if (curKingState == kingState.Tracing) {
            kingAnim.SetBool("isWalking", true);
            kingAnim.SetBool("isStamping",false);
            // 没捡小鸟朝玩家走
            if (birdItem.activeSelf) {
                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(target.position.x, this.transform.position.y), speed * Time.deltaTime);                        
            }
            else {
                winningCount ++;
                if (winningCount < 2) {
                    curKingState = kingState.Stamping;
                    isShaked = true;
                } 
                else {
                    curKingState = kingState.Breathing;
                }
            }
        }
    }
    //跺脚
    void KingStompMethod() {
        if (curKingState == kingState.Stamping && !GameManager.instance.IsDialogShow()) {
           if (isShaked) {
                kingAnim.SetBool("isWalking", false);
                kingAnim.SetBool("isStamping",true);
                CameraShake.ShakeCamera(3f,1f);
                isShaked = false;
            }
            // 跺脚脚落下，玩家摔倒
            if (isStomp) {
                GameManager.instance.stopMoving = true;
                //Debug.Log("跺脚animation + 女孩跌倒animation");
                //小鸟没有掉出，找位置
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
                isStomp = false;
            }
            // 跺脚动画播完
            else if (isEndstomp && playerAnim.GetBool("Falling") == false) {
                curKingState = kingState.Tracing;
                isEndstomp = false;
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
                    Dialog.PrintDialog("Lv4Part2Trace01");
                    kingAnim.SetBool("isWalking", false);
                    kingAnim.SetBool("isFaceR", false);
                    isStartThrow = true;
                }
                else {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(32.2f, this.transform.position.y), speed * Time.deltaTime);
                }
            }
            else if (isStartThrow && !GameManager.instance.IsDialogShow()) {
                throwTimer -= Time.deltaTime;
                if (throwTimer <= 0) {
                    if (winningCount == 0) {
                        dropPos = 25f;
                        throwPos.GetComponent<Transform>().position = new Vector2(dropPos, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(dropPos, throwItem.GetComponent<Transform>().position.y);
                        throwPos.SetActive(true);
                    }
                    else if (winningCount == 1) {
                        dropPos = 20f;
                        throwPos.GetComponent<Transform>().position = new Vector2(dropPos, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(dropPos, throwItem.GetComponent<Transform>().position.y);
                        throwPos.SetActive(true);
                    }
                    // 玩家位置
                    else if (winningCount == 2 || winningCount == 3) {
                        if (target.position.x <= 17.5f) {
                            dropPos = 17.5f;
                            throwPos.GetComponent<Transform>().position = new Vector2(dropPos, throwPos.GetComponent<Transform>().position.y);
                            throwItem.GetComponent<Transform>().position = new Vector2(dropPos, throwItem.GetComponent<Transform>().position.y);
                        } 
                        else if (target.position.x >= 29f) {
                            dropPos = 29f;
                            throwPos.GetComponent<Transform>().position = new Vector2(dropPos, throwPos.GetComponent<Transform>().position.y);
                            throwItem.GetComponent<Transform>().position = new Vector2(dropPos, throwItem.GetComponent<Transform>().position.y);
                        }
                        else {
                            Debug.Log("玩家位置");
                            throwPos.GetComponent<Transform>().position = new Vector2(target.position.x, throwPos.GetComponent<Transform>().position.y);
                            throwItem.GetComponent<Transform>().position = new Vector2(target.position.x, throwItem.GetComponent<Transform>().position.y);
                        }
                        throwPos.SetActive(true);
                    } 
                    else if (winningCount == 4 || winningCount == 5) {
                        dropPos = target.position.x;
                        throwPos.GetComponent<Transform>().position = new Vector2(dropPos + 2f, throwPos.GetComponent<Transform>().position.y);
                        throwPos01.GetComponent<Transform>().position = new Vector2(dropPos - 2f, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(dropPos + 2f, throwItem.GetComponent<Transform>().position.y);
                        throwItem01.GetComponent<Transform>().position = new Vector2(dropPos - 2f, throwItem.GetComponent<Transform>().position.y);
                        throwPos.SetActive(true);
                        throwPos01.SetActive(true);
                    }
                    else if (winningCount == 6 || winningCount == 7) {
                        dropPos = target.position.x;
                        throwPos.GetComponent<Transform>().position = new Vector2(dropPos, throwPos.GetComponent<Transform>().position.y);
                        throwPos01.GetComponent<Transform>().position = new Vector2(dropPos + 3f, throwPos.GetComponent<Transform>().position.y);
                        throwPos02.GetComponent<Transform>().position = new Vector2(dropPos - 3f, throwPos.GetComponent<Transform>().position.y);
                        throwItem.GetComponent<Transform>().position = new Vector2(dropPos, throwItem.GetComponent<Transform>().position.y);
                        throwItem01.GetComponent<Transform>().position = new Vector2(dropPos + 3f, throwItem.GetComponent<Transform>().position.y);
                        throwItem02.GetComponent<Transform>().position = new Vector2(dropPos - 3f, throwItem.GetComponent<Transform>().position.y);
                        throwPos.SetActive(true);
                        throwPos01.SetActive(true);
                        throwPos02.SetActive(true);
                    }
                    kingAnim.SetBool("isThrow",true);
                    //Debug.Log("砸一下");
                    Invoke("KingDropItem",1.5f);
                    curKingState = kingState.Throwed;
                }
            }
        } 
        else if (curKingState == kingState.Throwed) {
            // Debug.Log("准备dropitem");
            if (throwItem.activeSelf) {
                if (DropingItem.isThrowTrig && !isSEplayed) {
                    SoundManager.playSEOne("scepter",0.7f);
                    isSEplayed = true;
                }
                if (DropingItem.isThrowed) {
                    throwItem.SetActive(false);
                    throwPos.SetActive(false);
                    throwItem01.SetActive(false);
                    throwPos01.SetActive(false);
                    throwItem02.SetActive(false);
                    throwPos02.SetActive(false);
                    kingAnim.SetBool("isThrow",false);
                    throwTimer = Random.Range(1, 2);
                    winningCount ++;
                    curKingState = kingState.Throwing;
                }
            } else {
                isSEplayed = false;
            } 
        }
    }
    //drop动画
    void KingDropItem() {
        throwItem.SetActive(true);
        if (winningCount == 4 || winningCount == 5) {
            throwItem01.SetActive(true);
        } else if (winningCount == 6 || winningCount == 7) {
            throwItem01.SetActive(true);
            throwItem02.SetActive(true);
        }
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
    //来回走
    void KingUnTrancingMethod() {
    	if (curKingState == kingState.UnTracing) {
            // 女孩站起来 for part2
            // if(playerAnim.GetBool("Falling")) {
            //     playerAnim.SetBool("Falling", false);
            // } 
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

            kingAnim.SetBool("isWalking", true);
    		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y), speed * Time.deltaTime);
	        if (Vector2.Distance(this.transform.position, new Vector2(movePos[posNum].position.x, this.transform.position.y)) < 0.1f) {
	        	KingTurn();
		    }
    		//}	
    	}
    }

    bool CheckTraceMoveState() {
        // Debug.Log("CheckTraceMoveState");
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
    		if (winningCount >= s1WinTimes) {
    			sceneCount = 2;
	    		winningCount = 0;
                curKingState = kingState.Stop;
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
         		isMovingR = false;
        	} 
        	//正在向左走，需要转身
          	else {
                kingAnim.SetBool("isFaceR", false);
          		isMovingR = true;
          	}
        	turnWaitTime = wait;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GirlInGameMovement.curGirlState == GirlInGameMovement.girlState.UnHiding)
        {
            Debug.Log("游戏失败");
            loseReasonText.text = "The King caught you!";
            if (sceneCount != 2) {
                playerAnim.SetTrigger("Losed");
            } 
            else {
                GameFailed();
            } 
            isGameFailed = true;
        }
        if (collision.gameObject.name == "Bird") {
            Debug.Log("碰小鸟");
            //国王提起小鸟动画
            loseReasonText.text = "The King caught your friend!";
            GameFailed();
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

    void playStompSound() {
        SoundManager.playSEOne("stomp",0.7f);
        isStomp = true;
    }

    void stompEnd() {
        isEndstomp = true;
    }
}
