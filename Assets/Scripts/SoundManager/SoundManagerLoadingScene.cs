using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerLoadingScene : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    
    [Range(0f, 1f)]
    [SerializeField] private float volume;

    private void Start()
    {
        PlayAudioLoadScene();
    }

    private void PlayAudioLoadScene()
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
    }



}
