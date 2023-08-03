using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipState
{
    protected Ship ship;
    protected ShipStateMachine stateMachine;

    public ShipState(Ship ship)
    {
        this.ship = ship;
        this.stateMachine = ship.stateMachine;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
