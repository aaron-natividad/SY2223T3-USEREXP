using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliders : MonoBehaviour
{
    public static event Action<string, Slider> OnSliderChange;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;

    void Start()
    {
        AudioManager.instance?.GetMixerValues("Master", masterSlider);
        AudioManager.instance?.GetMixerValues("BGM", musicSlider);
        AudioManager.instance?.GetMixerValues("SFX", effectsSlider);
    }

    public void ChangeSlider(string volumeName)
    {
        if (volumeName == "Master")
        {
            OnSliderChange?.Invoke(volumeName, masterSlider);
        }
        else if (volumeName == "BGM")
        {
            OnSliderChange?.Invoke(volumeName, musicSlider);
        }
        else if (volumeName == "SFX")
        {
            OnSliderChange?.Invoke(volumeName, effectsSlider);
        }
    }
}
