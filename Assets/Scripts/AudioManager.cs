using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip hitSound;

    [SerializeField]
    private AudioSource soundsSource;

    [SerializeField]
    private AudioSource melodySource;



    private void Start()
    {
        PlayMelody();
        Hero.Stressed += OnHeroStressed;
    }


    protected override void OnDestroy()
    {
        Hero.Stressed -= OnHeroStressed;
    }

    private void OnHeroStressed()
    {
        PlayOneShotSound(hitSound);
    }
    

    public void PlayMelody()
    {
        if (melodySource == null) return;
        if (GameProgress.Instance.MusicIsOn == false) return;
        if (melodySource.clip != null) melodySource.Play();
    }

    public void PauseMelody()
    {
        if (melodySource == null) return;
        melodySource.Pause();
    }

    private void PlayOneShotSound(AudioClip audioClip)
    {
        if (GameProgress.Instance.MusicIsOn == false) return;
        soundsSource.PlayOneShot(audioClip);
    }

}
