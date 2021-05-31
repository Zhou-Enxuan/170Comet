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
        SoundManager.playBgm(0);
        if(GameManager.instance.playedLevel > 0)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
            GameObject.Find("Delete Button").GetComponent<Button>().interactable = true;
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
        LevelLoader.instance.LoadLevel("OP");
    }

    public void continueGame()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
        MenuUI.transform.Find("LevelSelect").gameObject.SetActive(true);
        ++currentLevelPage;
        updateLevelSelect();
    }

    public void DeleteGame()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void Credit()
    {
        audio[0].PlayOneShot(buttonSound, 0.1f);
        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
        MenuUI.transform.Find("LevelSelect").gameObject.SetActive(true);
        currentLevelPage = GameManager.instance.playedLevel + 2;
        updateLevelSelect();
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
            --currentLevelPage;
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
        if(currentLevelPage == 1)
        {
            LevelLoader.instance.LoadLevel("Level1");
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
            LevelLoader.instance.LoadLevel("Level4");
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
            GameObject.Find("Next").GetComponent<Button>().interactable = false;
        }
        //if(currentLevelPage > GameManager.instance.playedLevel)
        //{
            //Levels.transform.GetChild(0).Find("Text").GetComponent<Text>().text = "Credit";
            //Levels.transform.GetChild(1).Find("Text").GetComponent<Text>().text = "Credit";
            //Levels.transform.GetChild(0).Find("Button").gameObject.SetActive(false);
            //Levels.transform.GetChild(1).Find("Button").gameObject.SetActive(false);
            
        //}
        //else
        //{
        //    Levels.transform.GetChild(0).Find("Button").gameObject.SetActive(true);
        //    Levels.transform.GetChild(1).Find("Button").gameObject.SetActive(true);
        //    GameObject.Find("Next").GetComponent<Button>().interactable = true;

        //}

        if (currentLevelPage == GameManager.instance.playedLevel)
        {
            Levels.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void test()
    {
        MenuUI.transform.Find("Buttons").gameObject.SetActive(true);
        MenuUI.transform.Find("TitleButton").gameObject.SetActive(false);
    }
}
