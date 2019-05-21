using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public MusicScript[] sounds;
    public static AudioManager instance;

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        /*DontDestroyOnLoad(gameObject);*/

		foreach (MusicScript s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
        }
	}

    
    void Start()
    {
        /*
        print("music playing: tutorial theme");
        PlayMusic("ThemeTutorial");
        */
    }
	
    public void StopMusic(string name)
    {
        MusicScript s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Stop();
    }

	public void PlayMusic(string name)
    {
        print("music playing: " + name);
        MusicScript s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Play();
    }

    public float PlayMusicLengthBase(string name)
    {
        print("music playing: " + name);
        MusicScript s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return 0f;
        }
        s.source.PlayOneShot(s.clip);
        return s.clip.length;
    }
}
