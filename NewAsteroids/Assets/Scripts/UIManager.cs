using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Ship targetShip;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        pointsText.text = targetShip.points.ToString("000");

        timerText.text = "Timer: " + ((int)GameManager.timeLimit - (int)GameManager.timer);
    }
}