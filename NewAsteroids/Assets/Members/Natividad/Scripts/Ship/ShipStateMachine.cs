using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateMachine : MonoBehaviour
{
    [HideInInspector] public ShipState currentState;
    [HideInInspector] public MovingState movingState;
    [HideInInspector] public ShootingState shootingState;
    [HideInInspector] public DeathState deathState;
    [HideInInspector] public DisabledState disabledState;

    private Ship ship;

    private void Start()
    {
        ship = GetComponent<Ship>();
        movingState = new MovingState(ship);
        shootingState = new ShootingState(ship);
        deathState = new DeathState(ship);
        disabledState = new DisabledState(ship);
        SetState(movingState);
    }

    private void FixedUpdate()
    {
        currentState?.OnUpdate();
    }

    public void SetState(ShipState state)
    {
        currentState?.OnExit();
        currentState = state;
        state.OnEnter();
    }
}
