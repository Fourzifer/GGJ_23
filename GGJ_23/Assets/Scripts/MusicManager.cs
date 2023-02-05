using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip CalmMusic;
    public AudioClip IntenseMusic;

    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        PlayCalmMusic();
    }

    public void PlayCalmMusic()
    {
        AudioSource.clip = CalmMusic;
    }

    // Update is called once per frame
    public void PlayIntenseMusic()
    {
        AudioSource.clip = IntenseMusic;
    }
}
