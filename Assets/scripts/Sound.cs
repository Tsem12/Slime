using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void PlaySfx(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ChangeSound(float volume)
    {
        if (volume <= 1 && volume >= 0)
        {
            audioSource.volume = volume;
        }
    }
}
