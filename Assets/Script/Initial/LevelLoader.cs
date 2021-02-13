using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }
    void Awake()
    {
        if (instance != null) {
    		GameObject.Destroy(instance);
    	}
    	else {
    		instance = this;
    	}
    }
    public void LoadLevel (string sceneName) {
        Debug.Log(sceneName);
        gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    IEnumerator LoadAsynchronously (string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) {
            Debug.Log(operation.progress);
            yield return null; 
        }
        gameObject.SetActive(false);
    }
}
