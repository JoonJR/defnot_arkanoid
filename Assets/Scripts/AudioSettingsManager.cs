using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            // Initialize sliders with saved volumes or default values
            musicVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicVolumeKey, 0.5f);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat(AudioManager.SFXVolumeKey, 0.5f);

            // Add listeners
            musicVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    public void SetMusicVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(musicVolumeSlider.value);
        }
    }

    public void SetSFXVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(sfxVolumeSlider.value);
        }
    }
}