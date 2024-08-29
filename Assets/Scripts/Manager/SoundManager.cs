using System;
using UnityEngine;
using UnityEngine.Rendering;
using static GameIcons;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource _source;
    [SerializeField] private GameIcons _sounds;


    public void Initialize()
    {
        Launch = null;
        Launch += PlaySound;
        LaunchOwn += PlaySound;
    }
    public static event Action<SoundType> Launch;
    public static event Action<AudioClip> LaunchOwn;
    public static void LaunchSound(SoundType soundType)
    {
        Launch?.Invoke(soundType);
    }
    public static void LaunchSound(AudioClip soundType)
    {
        LaunchOwn?.Invoke(soundType);
    }
    private void OnDestroy()
    {

        Launch -= PlaySound;
        LaunchOwn -= PlaySound;
    }
    public void  PlaySound(SoundType soundType)
    {
        var clip = _sounds.GetClip(soundType);
        AudioSource soundObject = Instantiate(_source);
        soundObject.clip = clip;
        soundObject.Play();
        StartCoroutine( soundObject.GetComponent<DestroyAudioSorce>().LaunchDestroy(clip.length));

    }
    public void PlaySound(AudioClip soundType)
    {
        AudioSource soundObject = Instantiate(_source);
        soundObject.clip = soundType;
        soundObject.Play();
        StartCoroutine( soundObject.GetComponent<DestroyAudioSorce>().LaunchDestroy(soundType.length));

    }
}
