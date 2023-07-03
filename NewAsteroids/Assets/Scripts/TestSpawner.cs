using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    // test spawner to see if the game works ok

    public GameObject asteroidPrefab;

    public Vector2 boundsX;
    public Vector2 boundsY;

    public Vector2 asteroidVelocity;
    public float spawnInterval;

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    public IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            float spawnX = Random.Range(boundsX.x, boundsX.y);
            float spawnY = Random.Range(boundsY.x, boundsY.y);
            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            asteroid.GetComponent<Rigidbody2D>().velocity = asteroidVelocity;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
