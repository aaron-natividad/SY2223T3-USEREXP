using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public static event Action<int> OnLivesDepleted;
    public event Action<int> OnShipDeath;

    [Header("Ship Info")]
    public string shipName;
    public int playerNumber;

    [Header("Ship Sprite")]
    public GameObject spriteParent;
    public SpriteRenderer baseSprite;
    public SpriteRenderer colorSprite;
    public SpriteRenderer outlineSprite;
    public SpriteRenderer thrusterSprite;

    [Header("Health")]
    public int lives;
    public float respawnTime;
    [SerializeField] private GameObject deathParticlePrefab;
    
    [Header("Movement")]
    public float moveSpeed;
    public float moveAcceleration;
    [Range(0f, 1f)] public float rotateTime;

    // Components
    [HideInInspector] public ShipShooting shooting;
    [HideInInspector] public ShipStateMachine stateMachine;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Collider2D collision;
    [HideInInspector] public Canvas shipUI;

    // Controls
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction fire;

    private void Awake()
    {
        shooting = GetComponent<ShipShooting>();
        stateMachine = GetComponent<ShipStateMachine>();
        rigidBody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
        shipUI = GetComponentInChildren<Canvas>();
        controls = new PlayerInputControls();
    }

    private void OnEnable()
    {
        move = playerNumber == 0 ? controls.Player1.Move : controls.Player2.Move;
        move.Enable();
        fire = playerNumber == 0 ? controls.Player1.Fire : controls.Player2.Fire;
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    public void SetPlayerNumber(int number)
    {
        move.Disable();
        fire.Disable();
        playerNumber = number;

        move = playerNumber == 0 ? controls.Player1.Move : controls.Player2.Move;
        move.Enable();
        fire = playerNumber == 0 ? controls.Player1.Fire : controls.Player2.Fire;
        fire.Enable();
    }

    public void EnableShip(bool isEnabled)
    {
        rigidBody.simulated = isEnabled;
        collision.enabled = isEnabled;
        shipUI.enabled = isEnabled;
        spriteParent.SetActive(isEnabled);
    }

    public IEnumerator CO_OnDeath()
    {
        EnableShip(false);
        lives--;
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        OnShipDeath?.Invoke(playerNumber);
        if (lives <= 0)
        {
            OnLivesDepleted?.Invoke(playerNumber);
        }
        else
        {
            yield return new WaitForSeconds(respawnTime);
            EnableShip(true);
            yield return null;
            stateMachine.SetState(stateMachine.movingState);
        }
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed += value;
        moveAcceleration += value;
    }
}
