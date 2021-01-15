using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip killBug;

    [SerializeField]
    private AudioSource soundsSource;

    [SerializeField]
    private AudioSource melodySource;

    [SerializeField]
    private AudioSource additionalAudioSource;

    private Dictionary<int, AudioClip> audioFiles = new Dictionary<int, AudioClip>();


    private void Start()
    {
        PlayMelody();
    }

    public void PlayMelody()
    {
        if (soundsSource == null) return;
        if (GameProgress.Instance.MusicIsOn == false) return;
        if (melodySource.clip != null) melodySource.Play();
    }

    public void PauseMelody()
    {
        if (soundsSource == null) return;
        melodySource.Pause();
    }

    private void PlayOneShotSound(AudioClip audioClip)
    {
        if (GameProgress.Instance.SoundIsOn == false) return;
        soundsSource.PlayOneShot(audioClip);
    }

}
