using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    private void Awake()
    {
        instance = this;
    }
    public void PlayThisSound(string clipName,string fileName,float volumeMultipler)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume *= volumeMultipler;
        audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + fileName+"/" + clipName, typeof(AudioClip)));
    }
}
