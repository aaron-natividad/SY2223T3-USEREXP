using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;

    [SerializeField] private int reloadTimer;
    [SerializeField] private int maxBulletCount;
    [SerializeField] private int currentBulletCount;

    private bool canFire;

    private void Start()
    {
        reloadTimer = 1;
        canFire = true;
        currentBulletCount = maxBulletCount;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire") && canFire)
        {
            Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            currentBulletCount--;
        }

        if (currentBulletCount <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            canFire = false;
            StartCoroutine(Reload(reloadTimer));
        }
    }

    IEnumerator Reload(int reloadTimer)
    {
        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
        currentBulletCount = maxBulletCount;
    }
}