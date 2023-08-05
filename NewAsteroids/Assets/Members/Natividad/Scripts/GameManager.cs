using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
    [Header("UI")]
    public PreGameUI preGameUI;
    public GameUI gameUI;

    [Header("Spawners")]
    public ShipSpawner shipSpawner;
    public ItemSpawner itemSpawner;

    [Header("Timers")]
    [SerializeField] private int countdownSeconds;
    [SerializeField] private int timeLimitSeconds;
    private int remainingTimeSeconds;

    private void OnEnable()
    {
        ShipHealth.OnLivesDepleted += EndGame;
    }

    private void OnDisable()
    {
        ShipHealth.OnLivesDepleted -= EndGame;
    }

    private void Start()
    {
        selectionControls.enabled = false;
        
        StartCoroutine(CO_StartCountdown());
    }

    public void CheckWinner()
    {
        if (shipSpawner.ships[0].health.GetLives() > shipSpawner.ships[1].health.GetLives())
            EndGame(1);
        else if (shipSpawner.ships[0].health.GetLives() < shipSpawner.ships[1].health.GetLives())
            EndGame(0);
        else
            EndGame(2);
    }

    public void EndGame(int losingPlayer)
    {
        itemSpawner.DisableSpawning();
        StopAllCoroutines();
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
        
        selectionControls.enabled = true;
        preGameUI.ActivatePanel("EndGamePanel");
    }

    private void EnableShips(bool isEnabled)
    {
        foreach (Ship ship in shipSpawner.ships)
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
        yield return null;
        EnableShips(false);
        cover.FadeCover(false, 0.5f);
        preGameUI.ActivatePanel("CountdownPanel");
        yield return new WaitForSeconds(1f); // Delay

        foreach (Ship ship in shipSpawner.ships)
            gameUI.SetPlayerInfo(ship);

        for (int i = countdownSeconds; i > 0; i--)
        {
            preGameUI.SetCountdown(i);
            yield return new WaitForSeconds(1f);
        }
        preGameUI.SetCountdown("Start!");
        yield return new WaitForSeconds(1f);

        gameUI.Activate();
        EnableShips(true);
        StartCoroutine(CO_RunTimer());
    }

    private IEnumerator CO_RunTimer()
    {
        itemSpawner.EnableSpawning();
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