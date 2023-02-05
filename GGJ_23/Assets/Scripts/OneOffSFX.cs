using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class OneOffSFX : MonoBehaviour
{
    public static OneOffSFX Spawn(Vector3 position, AudioClip clip, AudioMixerGroup mixerGroup, float volume, Transform parent = null)
    {
        GameObject obj = new GameObject(clip.name, new[] { typeof(AudioSource), typeof(OneOffSFX) } );
        obj.transform.position = position;
        obj.transform.parent = parent;
        var source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = mixerGroup;
        source.volume = volume;
        return obj.GetComponent<OneOffSFX>();
    }

    AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        Source.loop = false;
        Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Source.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
