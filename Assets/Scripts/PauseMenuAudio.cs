using UnityEngine;
using UnityEngine.UI;

public class PauseMenuAudio : MonoBehaviour
{
    public Slider pauseMusicVolumeSlider;
    public Slider pauseSFXVolumeSlider;

    private void OnEnable() 
    {
        if (AudioManager.Instance != null)
        {
            pauseMusicVolumeSlider.value = AudioManager.Instance.musicAudioSource.volume;
            pauseSFXVolumeSlider.value = AudioManager.Instance.sfxAudioSource.volume;

            pauseMusicVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            pauseSFXVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    private void OnDisable() 
    {
        pauseMusicVolumeSlider.onValueChanged.RemoveAllListeners();
        pauseSFXVolumeSlider.onValueChanged.RemoveAllListeners();
    }
}