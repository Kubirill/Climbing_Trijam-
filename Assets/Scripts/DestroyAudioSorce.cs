using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyAudioSorce : MonoBehaviour
{
    public IEnumerator LaunchDestroy(float delay)
    {
        yield return  new WaitForSeconds(delay);
        Destroy (gameObject);
    }
}
