using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button backButtom;

    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider EnvironmentVolumeSlider;

    private void Start()
    {
        backButtom.onClick.AddListener(ClosePanel);

        MasterVolumeSlider.onValueChanged.AddListener(onMasterVolumeSliderChanged);
        SFXVolumeSlider.onValueChanged.AddListener(onSFXVolumeSliderChanged);
        EnvironmentVolumeSlider.onValueChanged.AddListener(onEnvironmentVolumeSliderChanged);

        MasterVolumeSlider.SetValueWithoutNotify(GameMenager.Instance.AudioManager.GetMixerVolume(MixerGroup.Master));
        SFXVolumeSlider.SetValueWithoutNotify(GameMenager.Instance.AudioManager.GetMixerVolume(MixerGroup.SFX));
        EnvironmentVolumeSlider.SetValueWithoutNotify(GameMenager.Instance.AudioManager.GetMixerVolume(MixerGroup.Environment));
    }
    private void ClosePanel()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        this.gameObject.SetActive(false);
    }

    private void onMasterVolumeSliderChanged(float value)
    {
        GameMenager.Instance.AudioManager.SetMixerVolume(MixerGroup.Master, value);
    }

    private void onSFXVolumeSliderChanged(float value)
    {
        GameMenager.Instance.AudioManager.SetMixerVolume(MixerGroup.SFX, value);
    }
    private void onEnvironmentVolumeSliderChanged(float value)
    {
        GameMenager.Instance.AudioManager.SetMixerVolume(MixerGroup.Environment, value);
    }

}
