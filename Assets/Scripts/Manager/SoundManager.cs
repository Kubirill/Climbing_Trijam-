using System;
using UnityEngine;
using static GameIcons;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource _source;
    [SerializeField] private GameIcons _sounds;


    public void Initialize()
    {
        Launch = null;
        Launch += PlaySound;
    }
    public static event Action<SoundType> Launch;
    public static void LaunchSound(SoundType soundType)
    {
        Launch?.Invoke(soundType);
    }

    public void  PlaySound(SoundType soundType)
    {
        var clip = _sounds.GetClip(soundType);
        AudioSource soundObject = Instantiate(_source);
        soundObject.clip = clip;
        soundObject.Play();
         soundObject.GetComponent<DestroyAudioSorce>().LaunchDestroy(clip.length);

    }
}
