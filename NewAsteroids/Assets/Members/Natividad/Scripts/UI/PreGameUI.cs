using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreGameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI countdown;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void SetEnabled(bool isEnabled)
    {
        canvas.enabled = isEnabled;
    }

    public void SetCountdown(int count)
    {
        countdown.text = count.ToString();
    }

    public void SetCountdown(string message)
    {
        countdown.text = message;
    }

    public void ActivatePanel(string panelName)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(panel.name == panelName);
        }
    }
}
