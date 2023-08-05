using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : UIGroup
{
    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] public PlayerInfoUI[] playerInfo;

    public void SetPlayerInfo(Ship ship)
    {
        playerInfo[(int)ship.assignedPlayer].SetPlayerInfo(ship);
    }

    public void SetTimer(int[] timeValues)
    {
        timer.text = timeValues[0].ToString("00") + ":" + timeValues[1].ToString("00");
    }
}
