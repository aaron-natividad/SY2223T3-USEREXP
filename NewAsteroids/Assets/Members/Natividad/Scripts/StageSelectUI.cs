using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    [Header("Stage Select UI")]
    [SerializeField] private TextMeshProUGUI prompt;
    [SerializeField] public PlayerPrompt[] playerPrompts;

    public void SetPlayerInfo(Ship ship)
    {
        playerPrompts[(int)ship.assignedPlayer].SetPlayerInfo(ship);
    }

    public void SetPrompt(int count)
    {
        prompt.text = count.ToString();
    }

    public void SetPrompt(string message)
    {
        prompt.text = message;
    }
}
