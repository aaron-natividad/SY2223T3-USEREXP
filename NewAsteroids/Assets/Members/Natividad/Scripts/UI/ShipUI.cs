using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    [SerializeField] private ShipShooting shooting;
    [SerializeField] Image[] ammoBoxes;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;

    private void Update()
    {
        UpdateAmmoBoxes();
        transform.up = Vector2.up;
    }

    private void UpdateAmmoBoxes()
    {
        for(int i = 0; i < shooting.maxAmmo; i++)
        {
            ammoBoxes[i].color = i < shooting.ammo ? activeColor : inactiveColor;
        }
    }
}
