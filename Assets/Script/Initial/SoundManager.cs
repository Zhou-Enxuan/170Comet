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
   // bool isBGMplayed = false; //flip
    public static bool isChangVolume = false;
    public static int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    //     RoomBGM = Resources.Load<AudioClip>("Sound/RoomBGM");
    //     Level2BGM = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = Resources.Load<AudioClip>("Sound/level1");
        audioSources[1].clip = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangVolume) {
            ChangVolume(i);
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

    public static void playRoomBgm() {
        audioSources[0].volume = 0;
        isChangVolume = true;
        audioSources[0].Play();
    }

    public static void playLv2Bgm(int num){
        i = num;
        audioSources[i-1].Stop();
        audioSources[i].volume = 0;
        isChangVolume = true;
        audioSources[i].Play();
    }
}
