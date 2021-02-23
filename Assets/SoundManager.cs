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
    bool play = false;
    bool isChangVolume = true;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    //     RoomBGM = Resources.Load<AudioClip>("Sound/RoomBGM");
    //     Level2BGM = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = Resources.Load<AudioClip>("Sound/RoomBGM");
        audioSources[i].volume = 0;
        audioSources[0].Play();
        audioSources[1].clip = Resources.Load<AudioClip>("Sound/Level2MusicConcept");
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangVolume) {
            ChangVolume(i);
        }

        if (SceneManager.GetActiveScene().name == "Level2Winter" && !play) {
            audioSources[0].Stop();
            i = 1;
            audioSources[i].volume = 0;
            isChangVolume = true;
            audioSources[1].Play();
            play = true;
        }
    }

    void ChangVolume(int i) {
        audioSources[i].volume = Mathf.Lerp(audioSources[i].volume, 1 , Time.deltaTime * audioSpeed);
        if ( audioSources[i].volume > 0.95) {
            audioSources[i].volume = 1;
            isChangVolume = false;
        }
    }
}
