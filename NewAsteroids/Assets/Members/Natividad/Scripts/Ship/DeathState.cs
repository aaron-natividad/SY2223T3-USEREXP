using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : ShipState
{
    public DeathState(Ship ship) : base(ship)
    {

    }

    public override void OnEnter()
    {
        ship.StartCoroutine(ship.CO_OnDeath());
    }
}
