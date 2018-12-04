    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenuScript : MonoBehaviour {

    #region "Private"
    private Resolution[] resolutions;
    #endregion

    #region "Inspector"
    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider EffectVolume;

    public FullScreenScript screenScript;
    public HorizontalScrollScript resolutionScript;
    public QualityScrollScript qualityScrollScript;
    #endregion

   

    private void Start()
    {
        resolutions = Screen.resolutions;
    }

    public void ApplySettings()
    {
        SetResolution(resolutionScript.GetResolutionIndex());
        SetQuality(qualityScrollScript.GetQualityIndex());
        SetFullScreen(screenScript.GetIsFullscreen());
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume()
    {
        AudioManager.instance.MasterVolume = MasterVolume.value;
        AudioManager.instance.SoundEffectVolume = MasterVolume.value * EffectVolume.value;
        AudioManager.instance.BackgroundMusicVolume = MasterVolume.value * MusicVolume.value;
        SetSoundEffectSourceVolume();
        SetMusicVolume();
    }

    private void SetSoundEffectSourceVolume()
    {
        AudioManager.instance.SoundEffectVolume = MasterVolume.value * EffectVolume.value;
        AudioManager.instance.SoundEffectSource.volume = MasterVolume.value * EffectVolume.value;
    }

    public void SetMusicVolume()
    {
        AudioManager.instance.BackgroundAudioSource.volume = MusicVolume.value * MasterVolume.value;
        AudioManager.instance.BackgroundMusicVolume = MusicVolume.value * MasterVolume.value;
    }

    public void SetEffectVolume()
    {
       
        SetSoundEffectSourceVolume();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualtiyIndex)
    {
        QualitySettings.SetQualityLevel(qualtiyIndex);
    }

}
