using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask spawnCheckMask;
    [SerializeField] private float spawnCheckRadius;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject spawnParticle;
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private BoxCollider2D spawnBounds;

    private bool isSpawning = false;

    public void EnableSpawning()
    {
        StartCoroutine(CO_SpawnAtInterval());
    }

    public void DisableSpawning()
    {
        isSpawning = false;
    }

    private IEnumerator CO_SpawnAtInterval()
    {
        int spawnIndex;
        Vector2 spawnPos;
        isSpawning = true;

        while (isSpawning)
        {
            bool hasSpawned = false;
            yield return new WaitForSeconds(spawnInterval);
            while (!hasSpawned)
            {
                spawnPos.x = Random.Range(spawnBounds.offset.x - spawnBounds.size.x / 2f, spawnBounds.offset.x + spawnBounds.size.x / 2f);
                spawnPos.y = Random.Range(spawnBounds.offset.y - spawnBounds.size.y / 2f, spawnBounds.offset.y + spawnBounds.size.y / 2f);
                Collider2D[] overlapped = Physics2D.OverlapCircleAll(spawnPos, spawnCheckRadius, spawnCheckMask);

                if (overlapped.Length <= 0)
                {
                    spawnIndex = Random.Range(0, itemPrefabs.Length);
                    Instantiate(spawnParticle, new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity);
                    Instantiate(itemPrefabs[spawnIndex], new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity);
                    hasSpawned = true;
                }
            }
        }
    }
}
