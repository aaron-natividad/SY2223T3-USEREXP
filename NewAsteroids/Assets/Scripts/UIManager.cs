using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Ship targetShip;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Update()
    {
        pointsText.text = targetShip.points.ToString("000");
    }
}
