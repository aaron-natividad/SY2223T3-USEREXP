using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private Ship ship;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject shootParticlePrefab;
    [SerializeField] private Transform projectileSpawn;

    [Header("Shooting Parameters")]
    public float holdInterval;
    [Space(10)]
    public int maxAmmo;
    public int ammo;
    public float ammoRegenInterval;
    public float recoilPerBounce;

    private float ammoRegenTimer = 0;

    private void FixedUpdate()
    {
        if (ammo >= maxAmmo || ship.stateMachine.currentState == ship.stateMachine.shootingState) return;

        if (ammoRegenTimer <= 0)
        {
            ammo = Mathf.Min(ammo + 1, maxAmmo);
            ammoRegenTimer = ammoRegenInterval;
        }
        else
        {
            ammoRegenTimer -= Time.deltaTime;
        }
    }

    public void SpawnProjectile(int bounces)
    {
        ammo = Mathf.Max(ammo - bounces, 0);
        Instantiate(shootParticlePrefab, projectileSpawn.position, projectileSpawn.rotation);
        Projectile spawnedProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
        spawnedProjectile.Initialize(bounces);
    }
}