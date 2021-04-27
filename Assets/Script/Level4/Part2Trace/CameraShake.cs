using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	public static CinemachineVirtualCamera cvCamera;
	public static float shakeTimerTotal;
	public static float shakeTimer;
	public static float startIntensity;

    // Start is called before the first frame update
    void Awake()
    {
        cvCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (shakeTimer > 0) {
        	shakeTimer -= Time.deltaTime;
        	CinemachineBasicMultiChannelPerlin cvBasicPerlin = 
        		cvCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        	cvBasicPerlin.m_AmplitudeGain = 
        		Mathf.Lerp(startIntensity, 0f, shakeTimer/shakeTimerTotal);
        	//Debug.Log(shakeTimer);
		    
        } else {
        	CinemachineBasicMultiChannelPerlin cvBasicPerlin = 
        		cvCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        	cvBasicPerlin.m_AmplitudeGain = 0f;
        }

        if (KingControl.sceneCount == 0 && KingControl.isToNextScene && GameManager.instance.stopMoving) {
        	CameraMove(new Vector3(25.6f, 0f, -10f));
        } 
        else if (KingControl.sceneCount == 1&& KingControl.isToNextScene && GameManager.instance.stopMoving) {
        	CameraMove(new Vector3(7.65f, 0f, -10f));
        } 
        else if (KingControl.sceneCount == 2 && KingControl.isToNextScene && GameManager.instance.stopMoving) {
        	CameraMove(new Vector3(-9.85f, 0f, -10f));
        }
    }

    private void CameraMove(Vector3 targetPos) {
		if (Vector3.Distance(this.transform.position, targetPos) < 0.1f) {
        	this.transform.position = targetPos;
        	GameManager.instance.stopMoving = false;
    	} 
    	else
        {
    		this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 6f * Time.deltaTime);
    	}	
    }
    public static void ShakeCamera(float intensity, float time) {
    	//Debug.Log("camera shakeing");
    	CinemachineBasicMultiChannelPerlin cvBasicPerlin = 
    		cvCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    	cvBasicPerlin.m_AmplitudeGain = intensity;

    	startIntensity = intensity;
    	shakeTimerTotal = time;
    	shakeTimer = time;
    }
}
