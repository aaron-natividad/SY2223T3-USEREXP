using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledState : ShipState
{
    public DisabledState(Ship ship) : base(ship)
    {

    }

    public override void OnEnter()
    {
        ship.rigidBody.simulated = false;
    }
}
