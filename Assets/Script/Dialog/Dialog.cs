//By Huazhen Xu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Dialog : MonoBehaviour
{
    public static Text dialogText;
    public static Text nameText;
    public static GameObject dialog;
    public TextAsset textFile;

    public static int index;
    public static List<string> AllTextlist = new List<string>();
    public static List<string> CurrentTextlist = new List<string>();
    public static List<string> dialogList;
    public static bool startTyping;
    public static bool isTyping;
    public static bool istalking;
    public static string Line;
    public static int j;
    public float textspeed;
   
    bool isTimeline = false; //是否cg动画播放中

    void Awake() {
        //print("start" + textFile.text);
        var linetext = textFile.text.Split('\n');
        foreach (var line in linetext) {
            AllTextlist.Add(line);
        }
        dialog = GameObject.Find("DialogBox");
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
        nameText  = GameObject.Find("NameText").GetComponent<Text>();
        
    }

    void Start() {
        index = 2;
        dialog.SetActive(false);
        isTyping = false;
        istalking = false;
    }

    void Update() {
        if (startTyping) {
            //Stops the previous sentence
            StopAllCoroutines(); 
            StartCoroutine(Typing(Line));
        }
        //检测是否动画播放，用SignalAsset变值
        if (GameObject.Find("TimelineGameManager") != null && TimelineGameManager.isTimeline == true){
            isTimeline = true;
        } else {
            isTimeline = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isTimeline) {
        	if (isTyping) {
                StopAllCoroutines(); 
                dialogText.text = "";
                dialogText.text = Line;
                isTyping = false;
            } 
            else if (!NextPage()) {
                dialogText.text = "";
                HideDialog();
            }
        }
    }

    //check dialog has next sentence or not
    public static bool NextPage() {
    	if (index > CurrentTextlist.Count - 1) {
    			index = 2;
    			//Debug.Log("no more tips");
                // dialog.SetActive(false);
                dialogText.text = "";
                return false;
            }
            GetNameText(index);
            startTyping = true;
            index ++;
            return true;
    }

    //Call for starting dialog
    public static void PrintDialog(string objName) {
        Debug.Log("PrintDialog");
        GameManager.instance.stopMoving = true;
        istalking = true;
        j = 0;
    	CurrentTextlist.Clear();
        //if (AllTextlist.Contains(objName)) {
            // int j = AllTextlist.IndexOf(objName);
            //代替IndexOf
            for (int i  = 0; i < AllTextlist.Count; i++) {
                if (string.Compare(AllTextlist[i], objName, StringComparison.Ordinal) == 0) {
                    j = i;
                    break;
                }
            }
            //代替IndexOf

            //while (AllTextlist[j].CompareTo("---") != 0) {
            //代替.CompareTo
            while (string.Compare(AllTextlist[j], "---", StringComparison.Ordinal) != 0) {
            //代替.CompareTo
                CurrentTextlist.Add(AllTextlist[j]);
                j++;
            }
        //}
        GetNameText(1);
        startTyping = true;
        dialog.SetActive(true);
        //print("Set dialog active, index: " + index + ", list length: " + textlist2.Count);
    }

    public static void GetNameText(int index) {
        Line = CurrentTextlist[index];
        if (Line.Contains(": ")) {
            var charNameString = Line.Split(": "[0]);
            string charName = charNameString[0];
            Line = Line.Replace(charName + ": ", "");
            nameText.text = charName;
            ChangeNameColor(charName);
        }
    }

    public static void ChangeNameColor(string name) {        
        Color newColor;
        switch (name) {
            case "Esther":
                ColorUtility.TryParseHtmlString ("#00A8FF", out newColor);
                nameText.color = newColor;
                break;
            case "Nightingale":
                ColorUtility.TryParseHtmlString ("#E5FF00", out newColor);
                nameText.color = newColor;
                break;
            case "Tom":
                ColorUtility.TryParseHtmlString ("#13FF00", out newColor);
                nameText.color = newColor;
                // nameText.color = new Color(255f,112f,0f,255f);
                break;
            case "Arthur":
                ColorUtility.TryParseHtmlString ("#FF7C00", out newColor);
                nameText.color = newColor;
                break;
            case "Cowboy":
                ColorUtility.TryParseHtmlString ("#FFB350", out newColor);
                nameText.color = newColor;
                break;
            case "Jenny":
                ColorUtility.TryParseHtmlString ("#AE62FF", out newColor);
                nameText.color = newColor;
                break;
            case "Soldier":
                ColorUtility.TryParseHtmlString ("#FF8488", out newColor);
                nameText.color = newColor;
                break;
            case "King":
                ColorUtility.TryParseHtmlString ("#FF0000", out newColor);
                nameText.color = newColor;
                break;
        }
    }
    public static void HideDialog() {
        if (!TimelineGameManager.isTimeline) {
            GameManager.instance.stopMoving = false;
        }
        dialogText.text = "";
        istalking = false;
        dialog.SetActive(false);
    }

    //Typewriter effect
    IEnumerator Typing(string sentences) {
         startTyping = false;
         isTyping = true;
         dialogText.text = "";
         //print("sentences : " + sentences);
         foreach(char letter in sentences.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(textspeed);
         }
         isTyping = false; 
    }
    

}
