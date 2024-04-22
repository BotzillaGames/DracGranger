using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public enum AudioType { Click, Seed, Water, Cut, Fire }

    public AudioClip fire;

    public AudioMixerGroup master;

    public AudioClip breakRose;

    public AudioClip paperOpen;

    public AudioClip paperClose;

    public AudioClip[] audios;

    public void GenerateAudio(AudioType audioType)
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = audios[(int)audioType];
        newAudioSource.Play();

        StartCoroutine(GarbageAudio(newAudioSource));
    }

    public void PlayFire()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = audios[(int)AudioType.Fire];
        newAudioSource.Play();

        StartCoroutine(BurningSound());
    }

    public void BreakRose()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = breakRose;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
    }

    public void PaperOpen()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = paperOpen;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
    }

    public void PaperClose()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = paperClose;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
    }

    private IEnumerator BurningSound()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.outputAudioMixerGroup = master;

        newAudioSource.clip = fire;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
        yield return new WaitForSeconds(4);
        LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((val) =>
        {
            if (newAudioSource) newAudioSource.volume = val;
        }).setEaseOutExpo().setOnComplete(() =>
        {
            newAudioSource.Stop();
        });
        yield return 0;
    }

    private IEnumerator GarbageAudio(AudioSource audioToDelete)
    {
        while (audioToDelete.isPlaying)
        {
            yield return 0;
        }
        Destroy(audioToDelete);
    }
}
