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
    }

    public static void ShakeCamera(float intensity, float time) {
    	CinemachineBasicMultiChannelPerlin cvBasicPerlin = 
    		cvCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    	cvBasicPerlin.m_AmplitudeGain = intensity;

    	startIntensity = intensity;
    	shakeTimerTotal = time;
    	shakeTimer = time;
    }
}
