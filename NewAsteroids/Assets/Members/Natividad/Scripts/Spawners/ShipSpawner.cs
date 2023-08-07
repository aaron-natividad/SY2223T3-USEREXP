using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipSpawner : MonoBehaviour
{
    public static ShipSpawner instance;

    [SerializeField] private Transform spawnpointParent;

    [Header("Ship Spawning")]
    [SerializeField] private GameObject[] shipPrefabs; // prefabs have to be in order
    [SerializeField] private ShipType[] typeFallback;
    [SerializeField] private string[] assignedPlayerPrefs;
    

    [Header("Styles")]
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject[] projectiles;

    [Header("Debug")]
    [SerializeField] private bool fixedSpawning;
    [SerializeField] private bool infiniteLives;

    [HideInInspector] public List<Transform> spawnpoints = new List<Transform>();
    [HideInInspector] public List<Ship> ships = new List<Ship>();

    private void Awake()
    {
        instance = this;
    }

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
        int shipType;
        List<Transform> validSpawnpoints = new List<Transform>(spawnpoints);

        for(int i = 0; i < spawnAmount; i++)
        {
            spawnIndex = fixedSpawning ? 0 : Random.Range(0, validSpawnpoints.Count);

            if (PlayerPrefs.HasKey(assignedPlayerPrefs[i]))
                shipType = PlayerPrefs.GetInt(assignedPlayerPrefs[i]);
            else
                shipType = (int)typeFallback[i];

            Ship spawnedShip = Instantiate(shipPrefabs[shipType], validSpawnpoints[spawnIndex].position, Quaternion.identity).GetComponent<Ship>();
            spawnedShip.SetPlayer((AssignedPlayer)i);
            spawnedShip.SetStyle(colors[i], projectiles[i]);
            spawnedShip.health.infiniteLives = infiniteLives;

            ships.Insert(i, spawnedShip);
            validSpawnpoints.RemoveAt(spawnIndex);
        }
    }
}
