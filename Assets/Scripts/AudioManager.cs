using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip mainmenuMusic;
    public AudioClip gameMusic;

    public const string MusicVolumeKey = "MusicVolume";
    public const string SFXVolumeKey = "SFXVolume";

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    private void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        // Load saved volume settings or use default values
        musicAudioSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        sfxAudioSource.volume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f); 
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play the appropriate music based on the scene
        if (scene.name == "MainMenu" || scene.name == "Settings" || scene.name == "Difficulty" || scene.name == "Nickname" || scene.name == "HiScores")
        {
            
            PlayMusic(mainmenuMusic, true);
            
            
        }
        else if (scene.name != "MainMenu" || scene.name != "Settings" || scene.name != "Difficulty" || scene.name != "Nickname" || scene.name != "HiScores") 
        {
            PlayMusic(gameMusic, true);
        }
    }
   
    public void PlayMusic(AudioClip clip, bool loop)
    {
        // Check if the new clip is different from the currently playing clip
        if (musicAudioSource.clip != clip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.loop = loop;
            musicAudioSource.Play();
        }
        // If it's the same clip, just ensure that looping is set correctly
        else
        {
            musicAudioSource.loop = loop;
        }
    }
    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }
    public void PlayEffect(AudioClip clip)
    {
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
}


