using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip audio;

    public void Play()
    {
        AudioSource.PlayOneShot(audio, 1);
    }
}
