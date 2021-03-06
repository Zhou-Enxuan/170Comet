using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private GameObject MenuUI;

    [SerializeField]
    private GameObject Levels;

    private int currentLevelPage;
    // Start is called before the first frame update
    void Awake()
    {
        MenuUI = GameObject.Find("MenuUI");
        currentLevelPage = 0;
    }

    void Start()
    {

        if(GameManager.instance.playedLevel > 0)
        {
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = true;
            GameObject.Find("Delete Button").GetComponent<Button>().interactable = true;
        }

        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void continueGame()
    {
        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
        MenuUI.transform.Find("LevelSelect").gameObject.SetActive(true);
        ++currentLevelPage;
        updateLevelSelect();
    }

    public void DeleteGame()
    {
        MenuUI.transform.Find("Confirm").gameObject.SetActive(true);
    }

    public void Credit()
    {
        MenuUI.transform.Find("Buttons").gameObject.SetActive(false);
        MenuUI.transform.Find("LevelSelect").gameObject.SetActive(true);
        currentLevelPage = GameManager.instance.playedLevel + 2;
        updateLevelSelect();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void confirmDeleteO()
    {
        SaveSystem.DeleteGame();
        GameObject.Find("Continue Button").GetComponent<Button>().interactable = false;
        GameObject.Find("Delete Button").GetComponent<Button>().interactable = false;
        MenuUI.transform.Find("Confirm").gameObject.SetActive(false);

    }

    public void confirmDeleteX()
    {
        MenuUI.transform.Find("Confirm").gameObject.SetActive(false);
    }

    public void prevPage()
    {
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
        currentLevelPage += 2;
        updateLevelSelect();
    }

    public void leftStart()
    {
        if(currentLevelPage == 1)
        {
            LevelLoader.instance.LoadLevel("Level1");
        }
        else if(currentLevelPage == 3)
        {
            LevelLoader.instance.LoadLevel("Level2SummerRoom");
        }
    }

    public void rightStart()
    {
        if (currentLevelPage == 1)
        {
            LevelLoader.instance.LoadLevel("Level2Winter");
        }
        else if (currentLevelPage == 3)
        {
            LevelLoader.instance.LoadLevel("Level2Fall");
        }
    }

    private void updateLevelSelect()
    {
        Levels.transform.GetChild(1).gameObject.SetActive(true);

        if (currentLevelPage == 1)
        {
            Levels.transform.GetChild(0).Find("Text").GetComponent<Text>().text = "Level 1";
            Levels.transform.GetChild(1).Find("Text").GetComponent<Text>().text = "Level 2 Part 1";
        }

        else if(currentLevelPage == 3)
        {
            Levels.transform.GetChild(0).Find("Text").GetComponent<Text>().text = "Level 2 Part 2";
            Levels.transform.GetChild(1).Find("Text").GetComponent<Text>().text = "Level 2 Part 3";
        }

        if(currentLevelPage > GameManager.instance.playedLevel)
        {
            Levels.transform.GetChild(0).Find("Text").GetComponent<Text>().text = "Credit";
            Levels.transform.GetChild(1).Find("Text").GetComponent<Text>().text = "Credit";
            Levels.transform.GetChild(0).Find("Button").gameObject.SetActive(false);
            Levels.transform.GetChild(1).Find("Button").gameObject.SetActive(false);
            GameObject.Find("Next").GetComponent<Button>().interactable = false;
        }
        else
        {
            Levels.transform.GetChild(0).Find("Button").gameObject.SetActive(true);
            Levels.transform.GetChild(1).Find("Button").gameObject.SetActive(true);
            GameObject.Find("Next").GetComponent<Button>().interactable = true;

        }

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
