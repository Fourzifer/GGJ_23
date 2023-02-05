using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class OneOffSFX : MonoBehaviour
{
    public static OneOffSFX Spawn(Vector3 position, AudioClip clip, AudioMixerGroup mixerGroup, Transform parent = null)
    {
        GameObject obj = new GameObject(clip.name, new[] { typeof(AudioSource), typeof(OneOffSFX) } );
        obj.transform.position = position;
        obj.transform.parent = parent;
        obj.GetComponent<AudioSource>().clip = clip;
        obj.GetComponent<AudioSource>().outputAudioMixerGroup = mixerGroup;
        return obj.GetComponent<OneOffSFX>();
    }

    AudioSource Source;

    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        Source.loop = false;
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
