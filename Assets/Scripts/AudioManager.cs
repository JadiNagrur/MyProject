using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayAudio(int val)
    {
        audioSource.clip = audioClips[val];
        audioSource.Play();
    }
}
