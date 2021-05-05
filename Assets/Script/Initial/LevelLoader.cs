using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	public static LevelLoader instance { get; private set; }
    public Animator anim;
    private string scene;
    private bool isCanMove = false;

    void Awake()
    {
        if (instance != null) {
    		GameObject.Destroy(instance);
    	}
    	else {
    		instance = this;
    	}

        anim = this.GetComponent<Animator>();
    }

    public void LoadLevel (string sceneName) {
        gameObject.SetActive(true);
        scene = sceneName; 
        isCanMove = false;
        StartCoroutine(Loading());
    }
    public void LoadNext() {
        SceneManager.LoadScene(scene);
        anim.SetTrigger("start");
    }
    IEnumerator Loading() {
        GameManager.instance.stopMoving = true;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        if (scene == "Level2Fall" && GameObject.Find("DialogBox") != null) {
            GameManager.instance.stopMoving = true;
        } else if (scene == "Level4Trace" && GameObject.Find("DialogBox") != null) {
            GameManager.instance.stopMoving = true;
        } else {
            GameManager.instance.stopMoving = false;
        }
        
        

    }
    // public void LoadLevel (string sceneName) {
    //     Debug.Log(sceneName);
    //     gameObject.SetActive(true);
    //     StartCoroutine(LoadAsynchronously(sceneName));
    // }

    // IEnumerator LoadAsynchronously (string sceneName) {
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //     while (!operation.isDone) {
    //         Debug.Log(operation.progress);
    //         yield return null; 
    //     }
    //     gameObject.SetActive(false);
    // }
}
