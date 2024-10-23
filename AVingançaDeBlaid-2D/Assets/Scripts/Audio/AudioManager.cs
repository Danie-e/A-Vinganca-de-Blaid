using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource EnviromentAudioSource;
    [SerializeField] private SFXConfig[] SFXConfigs;

    private Dictionary<SFX, SFXConfig> SFXs;

    private void Awake()
    {
        SFXs = SFXConfigs.ToDictionary(sfxConfig => sfxConfig.type, sfxConfig => sfxConfig);
    }

    public void PlaySFX(SFX type)
    {
        if (SFXs.ContainsKey(type))
        {
            SFXConfig config = SFXs[type];
            SFXAudioSource.PlayOneShot(config.AudioClip, config.VolumeScale);
        }
    }
}

public enum SFX
{
    PayerWalk,
    PlayerJump,
    PlayerAttack,
    PlayerHurt,
    PlayerDeath,
    ButtonClick
}

[Serializable]
struct SFXConfig
{
    public SFX type;
    public AudioClip AudioClip;
    public float VolumeScale;
}