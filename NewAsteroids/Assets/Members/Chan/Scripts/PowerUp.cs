using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ShipSpecial>())
        {
            collision.GetComponent<ShipSpecial>().SetCanActivate(true);
            Destroy(gameObject);
        }
    }
}
