using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;

    public int maxBounces;

    public void SpawnProjectile(int bounces)
    {
        Projectile spawnedProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
        spawnedProjectile.Initialize(bounces);
    }
}