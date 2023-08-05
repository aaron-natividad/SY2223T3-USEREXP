using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreGameUI : UIGroup
{
    [Header("Pre Game UI")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI winnerMessage;
    [SerializeField] private TextMeshProUGUI countdown;

    public void SetWinnerMessage(string message)
    {
        winnerMessage.text = message;
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
        Activate();
        foreach (GameObject panel in panels)
        {
            panel.SetActive(panel.name == panelName);
        }
    }
}
