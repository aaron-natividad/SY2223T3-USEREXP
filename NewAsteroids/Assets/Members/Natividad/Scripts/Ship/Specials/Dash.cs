using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : ShipSpecial
{
    public float speedMultiplier;

    public override void Activate()
    {
        AudioManager.instance?.sfx.PlayOneShot(activateSound);
        StartCoroutine(CO_Dash());
    }

    private IEnumerator CO_Dash()
    {
        float initialMoveSpeed = ship.moveSpeed;
        ship.moveSpeed *= speedMultiplier;
        isActive = true;
        yield return new WaitForSeconds(activeTime);
        ship.moveSpeed = initialMoveSpeed;
        isActive = false;
    }
}
