using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioType { Click, Seed, Water, Cut, Fire }

    public AudioClip fire;

    public AudioClip breakRose;

    public AudioClip[] audios;

    public void GenerateAudio(AudioType audioType)
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        newAudioSource.clip = audios[(int)audioType];
        newAudioSource.Play();

        StartCoroutine(GarbageAudio(newAudioSource));
    }

    public void PlayFire()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        newAudioSource.clip = audios[(int)AudioType.Fire];
        newAudioSource.Play();

        StartCoroutine(BurningSound());
    }

    public void BreakRose()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        newAudioSource.clip = breakRose;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
    }

    private IEnumerator BurningSound()
    {
        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        newAudioSource.clip = fire;
        newAudioSource.Play();
        StartCoroutine(GarbageAudio(newAudioSource));
        yield return new WaitForSeconds(4);
        LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((val) =>
        {
            newAudioSource.volume = val;
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
