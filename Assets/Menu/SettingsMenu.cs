using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// menu opcji
/// </summary>
public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetQuality(5);
    }
    /// <summary>
    /// zmień rozdzielczość
    /// </summary>
    /// <param name="resolutionIndex">index</param>
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    /// <summary>
    /// zmień głośność
    /// </summary>
    /// <param name="volume">głośniść</param>
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    /// <summary>
    /// zmień jakość
    /// </summary>
    /// <param name="qualityInsex">index</param>
    public void SetQuality (int qualityInsex)
    {
        Debug.Log(qualityInsex);
        QualitySettings.SetQualityLevel(qualityInsex);
    }
    /// <summary>
    /// ustaw pełny ekran
    /// </summary>
    /// <param name="isFullscreen">Fullscreen</param>
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
  
    }

}
