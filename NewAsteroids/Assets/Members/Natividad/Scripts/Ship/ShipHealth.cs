using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipHealth : MonoBehaviour
{
    public static event Action<int> OnLivesDepleted;
    public event Action<int> OnShipDeath;

    [SerializeField] private Ship ship;
    [SerializeField] private GameObject deathParticlePrefab;

    [Header("Health Parameters")]
    [SerializeField] private int lives;
    [SerializeField] private float respawnTime;

    [Header("Debug")]
    public bool infiniteLives;

    public void TakeDamage()
    {
        ship.EnableShip(false);
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        if (!infiniteLives)
        {
            lives--;
            OnShipDeath?.Invoke((int)ship.assignedPlayer);
        }

        if (lives <= 0)
            OnLivesDepleted?.Invoke((int)ship.assignedPlayer);
        else
            StartCoroutine(CO_Respawn());
    }

    public int GetLives()
    {
        return lives;
    }

    public IEnumerator CO_Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        if (ShipSpawner.instance != null)
        {
            int index = Random.Range(0, ShipSpawner.instance.spawnpoints.Count);
            transform.position = ShipSpawner.instance.spawnpoints[index].transform.position;
        }
        ship.EnableShip(true);
        yield return null;
        ship.stateMachine.SetState(ship.stateMachine.movingState);
    }
}
