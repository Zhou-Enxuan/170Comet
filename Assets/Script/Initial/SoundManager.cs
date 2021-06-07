using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	// public static AudioClip RoomBGM;
 //    public static AudioClip Level2BGM;
    public static AudioSource[] audioSources;
    public float audioSpeed;
    // 播放soundeffect的source number
    public static int soundEffectSource = 1;
   // bool isBGMplayed = false; //flip
    public static bool isChangVolume = false;
    public static int newBgm = 0;
    public static int curBgm = -1;
    public static AudioClip MenuBgm;
    public static AudioClip Lv1Bgm;
    public static AudioClip opBgm;
    public static AudioClip Lv2P1Bgm;
    public static AudioClip Lv2P102Bgm;
    public static AudioClip Lv2P2Bgm;
    public static AudioClip Lv2P3Bgm;
    public static AudioClip Lv2EndBgm;
    public static AudioClip Lv4P2Bgm;
    public static AudioClip Lv4TraceBgm;
    public static AudioClip Lv5Bgm;
    public static AudioClip endingBgm;
    public static AudioClip birdFlyOutSound;
    public static AudioClip paperSound;
    public static AudioClip scepterSound;
    public static AudioClip stompSound;

    // Start is called before the first frame update
    void Awake()
    {
    //     RoomBGM = Resources.Load<AudioClip>("Sound/RoomBGM");
    //     Level2BGM = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        MenuBgm = Resources.Load<AudioClip>("Sound/BGM/A_TitleMenu");
        opBgm = Resources.Load<AudioClip>("Sound/BGM/A_OPBgm");
        Lv1Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv1RoomBGM");
        Lv2P1Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv2BefoHorse");
        Lv2P102Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv2AfterHorse");
        Lv2P2Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv2RoomBGM");
        Lv2P3Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv3TownBGM");
        Lv2EndBgm = Resources.Load<AudioClip>("Sound/BGM/A_MemoryBgm");
        Lv4P2Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv4Part2");
        Lv4TraceBgm = Resources.Load<AudioClip>("Sound/BGM/A_Lv4Trace");
        Lv5Bgm = Resources.Load<AudioClip>("Sound/BGM/A_Level5BGM");
        endingBgm = Resources.Load<AudioClip>("Sound/BGM/A_EndingBGM");
        birdFlyOutSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_BirdFlyOut");
        paperSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_PaperSound");
        scepterSound =  Resources.Load<AudioClip>("Sound/SoundEffect/A_Scepter");
        stompSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_KingStomp");
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangVolume) {
            ChangVolume(0);
        }

        // if (SceneManager.GetActiveScene().name == "Level1" && !isBGMplayed) {
        //     PlayClip();

        // }
        // if (SceneManager.GetActiveScene().name == "Level2Winter" && isBGMplayed) {
        //     audioSources[i].Stop();
        //     i = 1;
        //     PlayClip();
            
        // }

    }

    void ChangVolume(int clipnum) {
        audioSources[clipnum].volume = Mathf.Lerp(audioSources[clipnum].volume, 1 , Time.deltaTime * audioSpeed);
        if ( audioSources[clipnum].volume > 0.95) {
            audioSources[clipnum].volume = 1;
            isChangVolume = false;
        }
    }

    // void PlayClip() {
    //     audioSources[i].volume = 0;
    //     isChangVolume = true;
    //     audioSources[i].Play();
    //     isBGMplayed = !isBGMplayed;
    // // }
    // public static void PlayTMBgm() {
    //     audioSources[0].volume = 0;
    //     isChangVolume = true;
    //     audioSources[0].Play();
    //     curBgm = 0;
    //     Debug.Log("play music");
    // }

    public static void playBgm(int num){
        if (SceneManager.GetActiveScene().name == "OP" || SceneManager.GetActiveScene().name == "Ending") {
            audioSources[0].loop = false;
        } else {
            audioSources[0].loop = true;
        }
        newBgm = num;
        if (newBgm != curBgm){
            audioSources[0].Stop();
            audioSources[0].volume = 0;
            isChangVolume = true;
            switch (num) {
                case 0:
                    audioSources[0].clip = MenuBgm;
                    break;
                case 1:
                    audioSources[0].clip = Lv1Bgm;
                    break;
                case 2:
                    audioSources[0].clip = Lv2P1Bgm;
                    break;
                case 3:
                    audioSources[0].clip = Lv2P102Bgm;
                    break;
                case 4:
                    audioSources[0].clip = Lv2P2Bgm;
                    break;
                case 5:
                    audioSources[0].clip = Lv2P3Bgm;
                    break;
                case 6:
                    audioSources[0].clip = Lv4P2Bgm;
                    break;
                case 7:
                    audioSources[0].clip = Lv4TraceBgm;
                    break;
                case 8:
                    audioSources[0].clip = Lv5Bgm;
                    break;
                case 9:
                    audioSources[0].clip = opBgm;
                    break;
                case 10: 
                    audioSources[0].clip = endingBgm;
                    break;
                case 11:
                    audioSources[0].clip = Lv2EndBgm;
                    break;
            }
            audioSources[0].Play();
            curBgm = num;
            GameManager.instance.bgmNum = num;
        }
    }

    public static void playSEOne(string name, float loud) {
        switch (name) {
            case "birdFlyOut":
                audioSources[soundEffectSource].PlayOneShot(birdFlyOutSound, loud);
                break;
            case "paper":
                audioSources[soundEffectSource].PlayOneShot(paperSound, loud);
                break;
            case "scepter":
                audioSources[soundEffectSource].PlayOneShot(scepterSound, loud);
                break;
            case "stomp": 
                audioSources[soundEffectSource].PlayOneShot(stompSound, loud);
                break;
        }
    }
}
