using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioClip musicClip;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip sfxClip;
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;

    protected override bool ShouldDestroyOnLoad => true;

    private void Start()
    {
        PlayMusicTheme();
    }

    private void OnEnable()
    {
        Observer.Instance.AddObserver(EventID.OnTurnOfMusic, StopMusicWhenPlayerDieAndPauseGame);
        Observer.Instance.AddObserver(EventID.OnSFXDeadSound, PlayerSFXSoundWhenDead);
    }

    private void OnDisable()
    {
        if (Observer.Instance != null)
        {
            Observer.Instance.RemoveObserver(EventID.OnTurnOfMusic, StopMusicWhenPlayerDieAndPauseGame);
            Observer.Instance.RemoveObserver(EventID.OnSFXDeadSound, PlayerSFXSoundWhenDead);

        }

    }

    public void PlayMusicTheme()
    {
        musicSource.clip = musicClip;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    private void StopMusicWhenPlayerDieAndPauseGame(object data)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    private void PlayerSFXSoundWhenDead(object data)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

}
