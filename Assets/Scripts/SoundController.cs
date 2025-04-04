using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayBackgroundMusic(bgMusicClip);
    }

    [SerializeField] private AudioSource bgMusicSource;
    [SerializeField] private AudioSource effectSource;
    //public AudioClip running;
    public AudioClip jumping;
    public AudioClip attack;
    public AudioClip die;

    public AudioClip bgMusicClip;
    public AudioClip bgMusicClip2;


    public void PlayBackgroundMusic(AudioClip bgMusic)
    {
        bgMusicSource.clip = bgMusic;
        bgMusicSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}