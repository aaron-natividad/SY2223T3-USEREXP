using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrompt : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image specialBase;
    [SerializeField] private Image specialIcon;

    [Header("Colors")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;

    public void SetPlayerInfo(Ship ship)
    {
        specialIcon.sprite = ship.special.specialIcon;
        ship.special.OnCanActivateChanged += SetSpecialUIActivated;
    }

    public void SetSpecialUIActivated(bool isActivated, int playerNumber)
    {
        specialIcon.color = isActivated ? activeColor : inactiveColor;
        specialBase.color = isActivated ? activeColor : inactiveColor;
    }
}
