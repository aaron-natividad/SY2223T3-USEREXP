using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void SetEnabled(bool isEnabled)
    {
        canvas.enabled = isEnabled;
    }

    public void SetTimer(int[] timeValues)
    {
        timer.text = timeValues[0].ToString("00") + ":" + timeValues[1].ToString("00");
    }
}
