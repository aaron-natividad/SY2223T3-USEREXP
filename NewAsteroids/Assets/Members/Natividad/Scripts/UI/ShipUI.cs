using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    [SerializeField] private ShipShooting shooting;

    [SerializeField] private TextMeshProUGUI playerNumber;
    [SerializeField] private Image[] ammoBoxes;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Update()
    {
        UpdateAmmoBoxes();
        transform.up = Vector2.up;
    }

    public void SetPlayerNumber(AssignedPlayer assignedPlayer)
    {
        playerNumber.text = "P" + ((int)assignedPlayer + 1);
    }

    private void UpdateAmmoBoxes()
    {
        for(int i = 0; i < shooting.maxAmmo; i++)
        {
            ammoBoxes[i].color = i < shooting.ammo ? activeColor : inactiveColor;
        }
    }
}
