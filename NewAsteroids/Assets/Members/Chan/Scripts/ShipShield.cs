using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : ShipSpecial
{
    [SerializeField] GameObject shieldCollider;

    public override void Activate()
    {
        AudioManager.instance?.sfx.PlayOneShot(activateSound);
        StartCoroutine(CO_Shield());
    }

    private IEnumerator CO_Shield()
    {
        shieldCollider.SetActive(true);
        isActive = true;
        yield return new WaitForSeconds(activeTime);
        shieldCollider.SetActive(false);
        isActive = false;
    }
    
}
