using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void playAudio(AudioClip audio)
    {

        audioSource.clip = audio;
        audioSource.Play();

    }  

}
