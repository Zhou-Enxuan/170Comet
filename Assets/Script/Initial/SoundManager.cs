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
    public static int soundEffectSource = 8;
   // bool isBGMplayed = false; //flip
    public static bool isChangVolume = false;
    public static int newBgm = 0;
    public static int curBgm;
    public static AudioClip birdFlyOutSound;
    public static AudioClip paperSound;

    // Start is called before the first frame update
    void Awake()
    {
    //     RoomBGM = Resources.Load<AudioClip>("Sound/RoomBGM");
    //     Level2BGM = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = Resources.Load<AudioClip>("Sound/BGM/A_TitleMenu");
        audioSources[1].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv1RoomBGM");
        audioSources[2].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv2BefoHorse");
        audioSources[3].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv2AfterHorse");
        audioSources[4].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv2RoomBGM");
        audioSources[5].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv3TownBGM");
        audioSources[6].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv4Part2");
        audioSources[7].clip = Resources.Load<AudioClip>("Sound/BGM/A_Lv4Trace");
        birdFlyOutSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_BirdFlyOut");
        paperSound = Resources.Load<AudioClip>("Sound/SoundEffect/A_PaperSound");
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangVolume) {
            ChangVolume(newBgm);
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
    // }
    public static void PlayTMBgm() {
        audioSources[0].volume = 0;
        isChangVolume = true;
        audioSources[0].Play();
        curBgm = 0;
        Debug.Log("play music");
    }

    public static void playBgm(int num){
        newBgm = num;
        if (newBgm != curBgm){
            audioSources[curBgm].Stop();
            audioSources[newBgm].volume = 0;
            isChangVolume = true;
            audioSources[newBgm].Play();
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
        }
    }
}
