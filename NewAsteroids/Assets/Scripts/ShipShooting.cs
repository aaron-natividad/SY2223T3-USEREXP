using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;

    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
        }
    }
}
