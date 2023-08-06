using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShield : ShipSpecial
{
    [SerializeField] GameObject shieldCollider;

    public override void Activate()
    {
        StartCoroutine(CO_Shield());
    }

    private IEnumerator CO_Shield()
    {
        shieldCollider.SetActive(true);
        yield return new WaitForSeconds(activeTime);
        shieldCollider.SetActive(false);
    }
    
}
