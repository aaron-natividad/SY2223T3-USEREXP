using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PreGameUI preGameUI;
    [SerializeField] private GameUI gameUI;
    [Space(10)]
    [SerializeField] private int countdownSeconds = 3;
    [SerializeField] private int timeLimitSeconds = 120;

    private int remainingTimeSeconds;

    private void Start()
    {
        StartCoroutine(CO_StartCountdown());
    }

    private int[] ConvertTimeValues(int seconds)
    {
        int[] timeValues = new int[2];
        timeValues[0] = seconds / 60;
        timeValues[1] = seconds % 60;
        return timeValues;
    }

    private IEnumerator CO_StartCountdown()
    {
        preGameUI.SetEnabled(true);
        preGameUI.ActivatePanel("CountdownPanel");
        gameUI.SetEnabled(false);
        yield return new WaitForSeconds(1f); // Delay

        for(int i = countdownSeconds; i > 0; i--)
        {
            preGameUI.SetCountdown(i);
            yield return new WaitForSeconds(1f);
        }
        preGameUI.SetCountdown("Start!");
        yield return new WaitForSeconds(1f);

        preGameUI.SetEnabled(false);
        gameUI.SetEnabled(true);
        StartCoroutine(CO_RunTimer());
    }

    private IEnumerator CO_RunTimer()
    {
        remainingTimeSeconds = timeLimitSeconds;

        while (remainingTimeSeconds > 0)
        {
            gameUI.SetTimer(ConvertTimeValues(remainingTimeSeconds));
            yield return new WaitForSeconds(1f);
            remainingTimeSeconds--;
        }
        gameUI.SetTimer(ConvertTimeValues(0));
        preGameUI.SetEnabled(true);
        preGameUI.ActivatePanel("EndGamePanel");
        gameUI.SetEnabled(false);
    }
}