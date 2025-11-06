using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float vfxVolume = 1f;

    public AudioSource bgmSource;
    private List<AudioSource> vfxSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            LoadVolumeSettings(); 
        }

    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("VFXVolume", vfxVolume);
        PlayerPrefs.Save();
    }
    public void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        vfxVolume = PlayerPrefs.GetFloat("VFXVolume", 1f);

        UpdateAllVolumes(); 
    }


    public void RegisterVFXSource(AudioSource source)
    {
        if (!vfxSources.Contains(source))
        {
            vfxSources.Add(source);
            source.volume = vfxVolume * masterVolume;
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        UpdateAllVolumes();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        if (bgmSource != null)
            bgmSource.volume = bgmVolume * masterVolume;
    }

    public void SetVFXVolume(float value)
    {
        vfxVolume = value;
        UpdateVFXVolumes();
    }

    private void UpdateAllVolumes()
    {
        SetBGMVolume(bgmVolume);
        UpdateVFXVolumes();
    }

    private void UpdateVFXVolumes()
    {
        foreach (var source in vfxSources)
        {
            if (source != null)
                source.volume = vfxVolume * masterVolume;
        }
    }
}
