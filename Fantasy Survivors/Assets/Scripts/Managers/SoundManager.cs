using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip gameOverSound;

    private AudioSource musicSource;
    private AudioSource soundEffectsSource;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        AudioSource[] audioSources = GetComponents<AudioSource>();

        musicSource = audioSources[0];
        soundEffectsSource = audioSources[1];

        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        soundEffectsSource.volume = PlayerPrefs.GetFloat("SoundVolume");
    }


    public void SetMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

    public AudioSource GetSoundSource()
    {
        return soundEffectsSource;
    }

    public void PlayMusicClip(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlaySoundClip(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }
}
