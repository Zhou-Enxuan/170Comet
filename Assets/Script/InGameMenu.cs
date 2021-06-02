using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
	public static GameObject GameMenu;
    public static bool GameIsPaused = false;
    // Start is called before the first frame update
    void Awake()
    {
      GameMenu = GameObject.Find("InGameMenu");
      GameMenu.SetActive(false);
	  GameIsPaused = false;
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (!GameManager.instance.stopMoving && !GameManager.instance.IsDialogShow()) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Debug.Log("escape");
                    if (!GameIsPaused) {
                        GameMenu.SetActive(true);
                        GameIsPaused = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    } else {
                        GameMenu.SetActive(false);
                        GameIsPaused = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }

                if (GameMenu.activeSelf) {
                    if (Input.GetKeyDown(KeyCode.Q)) {
                        Application.Quit();
                    }
                    else if (Input.GetKeyDown(KeyCode.B))
                    {
                        SceneManager.LoadScene("Menu");
                        GameMenu.SetActive(false);
                    }
                }
            }
        }
    }
}
