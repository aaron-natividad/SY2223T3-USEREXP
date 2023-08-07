using System.Collections;
using System.Collections.Generic;
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

        InitializeMixerValues("Master");
        InitializeMixerValues("BGM");
        InitializeMixerValues("SFX");
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

    private void InitializeMixerValues(string volumeName)
    {
        if (PlayerPrefs.HasKey(volumeName))
        {
            mainMixer.SetFloat(volumeName, PlayerPrefs.GetFloat(volumeName));
        }
        else
        {
            mainMixer.SetFloat(volumeName, 0);
            PlayerPrefs.SetFloat(volumeName, 0);
        }
    }
}
