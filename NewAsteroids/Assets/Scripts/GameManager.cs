using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PreGameUI preGameUI;
    [SerializeField] private GameUI gameUI;
    [Space(10)]
    [SerializeField] private Ship[] ships;
    [Space(10)]
    [SerializeField] private int countdownSeconds = 3;
    [SerializeField] private int timeLimitSeconds = 120;

    private int remainingTimeSeconds;

    private void OnEnable()
    {
        Ship.OnLivesDepleted += EndGame;
    }

    private void OnDisable()
    {
        Ship.OnLivesDepleted -= EndGame;
    }

    private void Start()
    {
        StartCoroutine(CO_StartCountdown());
        foreach (Ship ship in ships)
        {
            gameUI.SetPlayerInfo(ship);
        }
    }

    public void CheckWinner()
    {
        if (ships[0].lives > ships[1].lives)
            EndGame(1);
        else if (ships[0].lives < ships[1].lives)
            EndGame(0);
        else
            EndGame(2);
    }

    public void EndGame(int losingPlayer)
    {
        EnableShips(false);

        switch (losingPlayer)
        {
            case 0:
                preGameUI.SetWinnerMessage("P2 WINS");
                break;
            case 1:
                preGameUI.SetWinnerMessage("P1 WINS");
                break;
            default:
                preGameUI.SetWinnerMessage("DRAW");
                break;
        }
        
        preGameUI.SetEnabled(true);
        preGameUI.ActivatePanel("EndGamePanel");
        gameUI.SetEnabled(false);
    }

    private void EnableShips(bool isEnabled)
    {
        foreach (Ship ship in ships)
        {
            if (isEnabled)
                ship.stateMachine.SetState(ship.stateMachine.movingState);
            else
                ship.stateMachine.SetState(ship.stateMachine.disabledState);
        }
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
        EnableShips(false);
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
        EnableShips(true);
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
        CheckWinner();
    }
}