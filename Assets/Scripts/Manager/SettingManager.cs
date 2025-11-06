using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider vfxSlider;

    void Start()
    {
        masterSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SetMasterVolume(value);
            AudioManager.Instance.SaveVolumeSettings();
        });

        bgmSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SetBGMVolume(value);
            AudioManager.Instance.SaveVolumeSettings();
        });

        vfxSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SetVFXVolume(value);
            AudioManager.Instance.SaveVolumeSettings();
        });
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        // Load current values from AudioManager
        masterSlider.value = AudioManager.Instance.masterVolume;
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        vfxSlider.value = AudioManager.Instance.vfxVolume;
    }
}
