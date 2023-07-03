using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDetection : MonoBehaviour
{
    public Projectile projectile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (projectile.type == ProjectileType.Target || projectile.type == ProjectileType.Bounced)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else if (collision.CompareTag("Target"))
        {
            if (projectile.type == ProjectileType.Ship || projectile.type == ProjectileType.Bounced)
            {
                collision.GetComponent<Target>().DoTargetBehavior();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
