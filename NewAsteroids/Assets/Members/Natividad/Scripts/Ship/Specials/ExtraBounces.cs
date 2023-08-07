using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBounces : ShipSpecial
{
    public override void Activate()
    {
        AudioManager.instance?.sfx.PlayOneShot(activateSound);
        ship.shooting.extraBounces = 3;
    }
}
