using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBounces : ShipSpecial
{
    public override void Activate()
    {
        ship.shooting.extraBounces = 3;
    }
}
