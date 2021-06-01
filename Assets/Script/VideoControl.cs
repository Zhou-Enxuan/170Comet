 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.Video;
 using UnityEngine.SceneManagement;
 
 public class VideoControl : MonoBehaviour
 { 
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    public bool NeedPrint = true;
    void Start() 
    {
      if (SceneManager.GetActiveScene().name == "OP") {
        SoundManager.playBgm(9);
      }else if (SceneManager.GetActiveScene().name == "Ending") {
        SoundManager.playBgm(10);
      }  
      VideoPlayer.loopPointReached += LoadScene;
    }
     
    void LoadScene(VideoPlayer vp)
    {
      LevelLoader.instance.LoadLevel(SceneName);
    }

    void FixedUpdate(){
      if(VideoPlayer.time == 10.0 && NeedPrint){
        Dialog.PrintDialog("OP");
        NeedPrint = false;
      }
    }
  }
