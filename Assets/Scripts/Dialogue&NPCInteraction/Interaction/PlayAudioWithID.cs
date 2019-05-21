using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudioWithID : MonoBehaviour {

    public bool playModuleLines = true;

    public AudioClip[] audioClips;


    public void playAudio(string id)
    {
        foreach(AudioClip audioClip in audioClips)
        {
            string audioID = audioClip.name.Substring(0,3);
            if(audioID == id)
            {
                Debug.Log(" id modulu " + id);
                if (playModuleLines == true)
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.clip = audioClip;
                    audio.Play();
                }
                break;
            }
        }
    }

    public void stopAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio.isPlaying) { audio.Stop(); }

    }


}
