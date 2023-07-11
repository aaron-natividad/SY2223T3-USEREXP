using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHandler : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;

    [Space(10)]
    [SerializeField] private int pointsOnScreen;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private void OnEnable()
    {
        Point.OnCollect += SpawnNewPoint;
    }

    private void OnDisable()
    {
        Point.OnCollect -= SpawnNewPoint;
    }

    private void Start()
    {
        for(int i = 0; i < pointsOnScreen; i++)
        {
            SpawnNewPoint();
        }
    }

    public void SpawnNewPoint()
    {
        float spawnX = Random.Range(minBounds.x, maxBounds.x);
        float spawnY = Random.Range(minBounds.y, maxBounds.y);
        Vector2 spawnPos = new Vector2(spawnX, spawnY);

        Instantiate(pointPrefab, spawnPos, Quaternion.identity);
    }
}
