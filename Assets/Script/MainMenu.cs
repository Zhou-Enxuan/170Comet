using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private GameObject MenuUI;

    private event Action CallBack; 

    [SerializeField]
    private GameObject Levels;

    [SerializeField]
    private int currentLevelPage;

    private AudioSource[] audio;
    private AudioClip buttonSound;


    // Start is called before the first frame update
    void Awake()
    {
        MenuUI = GameObject.Find("MenuUI");
        currentLevelPage = 0;
        buttonSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_MenuButton");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SoundManager.playBgm(0);
        if(GameManager.instance.playedLevel > 0)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
            MenuUI.transform.Find("SettingMenu").Find("Delete Button").GetComponent<Button>().interactable = true;
        }

        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);

        audio = this.gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startGame()
    {
        CallBack = startGameEvent;
        StartCoroutine(ButtonSound());
    }

    private void startGameEvent() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LevelLoader.instance.LoadLevel(GameManager.instance.continueLevel);
    }

    public void continueGame()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
        MenuUI.transform.Find("LevelSelect").gameObject.SetActive(true);
        currentLevelPage = 1;
        updateLevelSelect();
    }

    public void Setting()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("SettingMenu").gameObject.SetActive(true);

    }

    public void SettingBack()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("SettingMenu").gameObject.SetActive(false);

    }

    public void DeleteGame()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void Credit()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Credit").gameObject.SetActive(true);
    }

    public void Credit_back()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Credit").gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        Application.Quit();
    }

    public void confirmDeleteO()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        SaveSystem.DeleteGame();
        GameObject.Find("Continue Button").GetComponent<Button>().interactable = false;
        GameObject.Find("Delete Button").GetComponent<Button>().interactable = false;
        MenuUI.transform.Find("Confirm").gameObject.SetActive(false);

    }

    public void confirmDeleteX()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Confirm").gameObject.SetActive(false);
    }

    public void prevPage()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        if (currentLevelPage <= 1)
        {
            MenuUI.transform.Find("Buttons").gameObject.SetActive(true);
            MenuUI.transform.Find("LevelSelect").gameObject.SetActive(false);
        }
        else
        {
            currentLevelPage -= 2;
            updateLevelSelect();
        }
    }

    public void nextPage()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        currentLevelPage += 2;
        updateLevelSelect();
    }

    public void leftStart()
    {
        CallBack = leftStartEvent;
        StartCoroutine(ButtonSound());
    }

    private void leftStartEvent() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (currentLevelPage == 1)
        {
            LevelLoader.instance.LoadLevel("OP");
        }
        else if(currentLevelPage == 3)
        {
            LevelLoader.instance.LoadLevel("Level2SummerRoom");
        }
        else if(currentLevelPage == 5)
        {
            LevelLoader.instance.LoadLevel("Level3OpenWindow");
        }
        else if(currentLevelPage == 7)
        {
            LevelLoader.instance.LoadLevel("Level4P2TL3");
        }
    }

    public void rightStart()
    {
        CallBack = rightStartEvent;
        StartCoroutine(ButtonSound());
    }

    private void rightStartEvent() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (currentLevelPage == 1)
        {
            LevelLoader.instance.LoadLevel("Level2Winter");
        }
        else if (currentLevelPage == 3)
        {
            LevelLoader.instance.LoadLevel("Level2Fall");
        }
        else if(currentLevelPage == 5)
        {
            LevelLoader.instance.LoadLevel("Level4StartTL");
        }
        else if(currentLevelPage == 7)
        {
            LevelLoader.instance.LoadLevel("Level5");
        }
    }

    IEnumerator ButtonSound() {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        yield return new WaitForSeconds(audio[0].clip.length);
        CallBack.Invoke();

    }

    private void updateLevelSelect()
    {
        Levels.transform.GetChild(1).gameObject.SetActive(true);

        if(currentLevelPage + 2 > GameManager.instance.playedLevel)
        {
            GameObject.Find("Next").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("Next").GetComponent<Button>().interactable = true;
        }

        if (currentLevelPage == 1)
        {
            Levels.transform.GetChild(0).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_01");
            Levels.transform.GetChild(1).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_02");
        }

        else if(currentLevelPage == 3)
        {
            Levels.transform.GetChild(0).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_03");
            Levels.transform.GetChild(1).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_04");
        }
        else if(currentLevelPage == 5)
        {
            Levels.transform.GetChild(0).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_01");
            Levels.transform.GetChild(1).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_02");
        }
        else if(currentLevelPage == 7)
        {
            Levels.transform.GetChild(0).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_03");
            Levels.transform.GetChild(1).GetComponent<Button>().image.sprite = Resources.Load<Sprite>("MenuUI/book_04");
        }

        if (currentLevelPage == GameManager.instance.playedLevel)
        {
            Levels.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void Book_Button()
    {
        if (GameManager.instance.playedLevel > 0)
        {
            audio[0].PlayOneShot(buttonSound, 0.1f);
            MenuUI.transform.Find("Buttons").gameObject.SetActive(true);
            MenuUI.transform.Find("TitleButton").gameObject.SetActive(false);
        }
        else
        {
            CallBack = Book_Intro_Event;
            StartCoroutine(ButtonSound());
        }
    }

    public void Book_Intro_Event()
    {
        MenuUI.transform.Find("Book_Intro").gameObject.SetActive(true);
        StartCoroutine(Book_Intro_fase());
    }
    IEnumerator Book_Intro_fase()
    {
        yield return new WaitForSeconds(2.5f);
        startGameEvent();
    }
}
