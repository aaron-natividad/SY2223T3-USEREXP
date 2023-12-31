using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : Target
{
    public GameObject projectilePrefab;
    public int projectileAmount;
    public float projectileRadius;

    public override void DoTargetBehavior()
    {
        AudioManager.instance?.sfx.PlayOneShot(targetSound);
        SpawnProjectiles();
        CameraController.instance.Shake(0.2f, 0.1f, 0.05f);
        Destroy(gameObject);
    }

    public void SpawnProjectiles()
    {
        float randomOffset = Random.Range(0, 2 * MathF.PI);
        for (int i = 0; i < projectileAmount; i++)
        {
            float radians = 2 * MathF.PI / projectileAmount * i;
            float vertical = MathF.Sin(radians + randomOffset);
            float horizontal = MathF.Cos(radians + randomOffset);

            Vector3 relativeSpawnPos = new Vector3(horizontal, vertical, 0);

            Vector3 spawnPos = transform.position + relativeSpawnPos * projectileRadius;
            Vector3 spawnDirection = spawnPos - transform.position;

            Projectile projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity).GetComponent<Projectile>();
            projectile.Initialize(2, 1);
            projectile.transform.up = spawnDirection;
        }
    }
}
