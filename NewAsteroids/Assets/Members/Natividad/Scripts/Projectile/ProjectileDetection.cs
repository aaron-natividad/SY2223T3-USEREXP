using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetection : MonoBehaviour
{
    [SerializeField] private Projectile projectile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>())
        {
            
            Ship ship = collision.GetComponent<Ship>();

            if (ship.playerNumber != projectile.ownerID)
            {
                ship.stateMachine.SetState(ship.stateMachine.deathState);
                Destroy(transform.parent.gameObject);
            }   
        }
        else if (collision.GetComponent<Target>())
        {
            Target target = collision.GetComponent<Target>();
            target.DoTargetBehavior();
            Destroy(transform.parent.gameObject);
        }
    }
}
