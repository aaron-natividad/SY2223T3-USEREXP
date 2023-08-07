using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mainMixer;
    [Space(10)]
    public AudioSource bgm;
    public AudioSource sfx;
    [Space(10)]
    [Range(-50,0)] public float bgmDefault;
    [Range(-50, 0)] public float sfxDefault;

    private void OnEnable()
    {
        AudioSliders.OnSliderChange += ChangeVolume;
    }

    private void OnDisable()
    {
        AudioSliders.OnSliderChange -= ChangeVolume;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeMixerValues("Master", 0, true);
        InitializeMixerValues("BGM", bgmDefault, true);
        InitializeMixerValues("SFX", sfxDefault, true);
    }

    public void ChangeVolume(string volumeName, Slider slider)
    {
        PlayerPrefs.SetFloat(volumeName, slider.value);
        mainMixer.SetFloat(volumeName, slider.value);
    }

    public void GetMixerValues(string volumeName, Slider volumeSlider)
    {
        if (PlayerPrefs.HasKey(volumeName))
        {
            if (volumeSlider != null) volumeSlider.value = PlayerPrefs.GetFloat(volumeName);
        }
        else
        {
            if (volumeSlider != null) volumeSlider.value = 0;
        }
    }

    private void InitializeMixerValues(string volumeName, float defaultValue, bool forceValue)
    {
        if (PlayerPrefs.HasKey(volumeName) && !forceValue)
        {
            mainMixer.SetFloat(volumeName, PlayerPrefs.GetFloat(volumeName));
        }
        else
        {
            mainMixer.SetFloat(volumeName, defaultValue);
            PlayerPrefs.SetFloat(volumeName, defaultValue);
        }
    }
}
