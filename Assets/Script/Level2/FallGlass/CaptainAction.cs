using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptainAction : MonoBehaviour
{
	public static bool isSoldierRun = false;
	public static bool isSoldierTrace = false;
	public GameObject glass;
	public GameObject mark;
	public GameObject questionMark;
	public GameObject hint;
	public GameObject fading;
	float whistleTime = 3f;
	float knockableTime = 1f;
	private bool isKnockable = false;	//玻璃是否可以被敲
	private bool isGameStart = false;	//游戏开始
	private bool isKnockedGlass = false; //是否敲过玻璃了
	private int sucCount = 0;
	private int failCount = 0;
	enum failState {CHANGED, UNCHANGE, NONE};
	failState curFailState;

    // Start is called before the first frame update
    void Awake()
    {
    	glass.SetActive(false);
    	fading.SetActive(false);
    	questionMark.SetActive(false);
    	mark.SetActive(false);
    	hint.SetActive(false);
    	Invoke("StartWhistle", 3f);
    }

    // Update is called once per frame
    void Update()
    {
    	if (isGameStart) {
    		Debug.Log("Start");
	        whistleTime -= Time.deltaTime;
	        if (whistleTime <= 0) {
	        	whistleTime = Random.Range(2, 4); //随机2-4秒
	        	isSoldierRun = !isSoldierRun;
	        	isKnockedGlass = false;
	        	knockableTime = 1f; //敲玻璃时间恢复
	        	// Debug.Log("士兵跑步/静止");
	        }
	        else {
	        	if(knockableTime <= 1f){
		        	knockableTime -= Time.deltaTime;
		        	if (knockableTime <= 0) {
		        		knockableTime = 2f;
		        		isKnockedGlass = false;
		        		isKnockable = false;
		        		mark.SetActive(false);
		        		hint.SetActive(false);
			        	// Debug.Log("不可以敲");
		        	} else {
		        		isKnockable = true;
			        	mark.SetActive(true);
			        	hint.SetActive(true);
			        	// Debug.Log("可以敲");
		        	}
		        }
	        }

	        if (Input.GetKeyDown(KeyCode.Space) && !isKnockedGlass) {
	        	if (isKnockable) {
	        		sucCount += 1;
	        		Debug.Log("敲成功");
	        	} 
	        	else {
	        		failCount += 1 ;
	        		curFailState = failState.UNCHANGE;
	        		Debug.Log("敲失败");
	        	}
	        	isKnockedGlass = true;
	        }

	    }

        if (failCount == 1 && curFailState == failState.UNCHANGE) {
        	questionMark.SetActive(true);
        	isSoldierRun = false;
        	whistleTime = Random.Range(3, 6); 
        	Debug.Log("重新计时");
        	curFailState = failState.CHANGED;
        }
        else if (failCount == 2 && curFailState == failState.UNCHANGE) {
        	curFailState = failState.CHANGED;
        	isGameStart = false;
        	isSoldierRun = false;
        	isSoldierTrace = true;
        	Debug.Log("fading");     
        	StartCoroutine(GameEnd());
        }

        if (sucCount == 5){
        	glass.SetActive(true);
        	Debug.Log("游戏成功");
        }
    }

    void StartWhistle() {
		isGameStart = true;
    }

    IEnumerator GameEnd() {
    	fading.SetActive(true);
    	yield return new WaitForSeconds(3f);
    	SceneManager.LoadScene("Level2Fall");
    }
}
