using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnpointParent;

    [Header("Ship Spawning")]
    [SerializeField] private GameObject[] shipPrefabs; // prefabs have to be in order
    [SerializeField] private ShipType[] typeFallback;

    [Header("Styles")]
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject[] projectiles;

    private List<Transform> spawnpoints = new List<Transform>();

    private void Start()
    {
        for(int i = 0; i < spawnpointParent.childCount; i++)
        {
            spawnpoints.Add(spawnpointParent.GetChild(i));
        }

        SpawnShips(2);
    }

    public void SpawnShips(int spawnAmount)
    {
        int spawnIndex;
        List<Transform> validSpawnpoints = new List<Transform>(spawnpoints);

        for(int i = 0; i < spawnAmount; i++)
        {
            spawnIndex = Random.Range(0, validSpawnpoints.Count);
            Ship spawnedShip = Instantiate(shipPrefabs[(int)typeFallback[i]], validSpawnpoints[spawnIndex].position, Quaternion.identity).GetComponent<Ship>();
            spawnedShip.SetPlayer((AssignedPlayer)i);
            spawnedShip.SetStyle(colors[i], projectiles[i]);

            GameManager.instance.ships.Insert(i, spawnedShip);
            validSpawnpoints.RemoveAt(spawnIndex);
        }
    }
}
