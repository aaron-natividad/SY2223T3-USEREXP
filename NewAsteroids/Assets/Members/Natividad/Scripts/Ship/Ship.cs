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
    public AssignedPlayer assignedPlayer;

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
    [HideInInspector] public ShipSpecial special;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Collider2D collision;
    [HideInInspector] public ShipUI shipUI;

    // Controls
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction fireAction;
    [HideInInspector] public InputAction specialAction;

    private void Awake()
    {
        shooting = GetComponent<ShipShooting>();
        stateMachine = GetComponent<ShipStateMachine>();
        special = GetComponent<ShipSpecial>();
        rigidBody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
        shipUI = GetComponentInChildren<ShipUI>();
        controls = new PlayerInputControls();
    }

    private void OnEnable()
    {
        SetControls((int)assignedPlayer);
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    public void SetStyle(Color color, GameObject projectile)
    {
        colorSprite.color = color;
        shooting.projectilePrefab = projectile;
    }

    public void SetPlayer(AssignedPlayer assignedPlayer)
    {
        DisableControls();
        this.assignedPlayer = assignedPlayer;
        SetControls((int)assignedPlayer);
        shipUI.SetPlayerNumber(assignedPlayer);
        EnableControls();
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

        OnShipDeath?.Invoke((int)assignedPlayer);
        if (lives <= 0)
        {
            OnLivesDepleted?.Invoke((int)assignedPlayer);
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

    private void EnableControls()
    {
        moveAction.Enable();
        fireAction.Enable();
        specialAction.Enable();
    }

    private void DisableControls()
    {
        moveAction.Disable();
        fireAction.Disable();
        specialAction.Disable();
    }

    private void SetControls(int shipID)
    {
        moveAction = shipID == 0 ? controls.Player1.Move : controls.Player2.Move;
        fireAction = shipID == 0 ? controls.Player1.Fire : controls.Player2.Fire;
        specialAction = shipID == 0 ? controls.Player1.Special : controls.Player2.Special;
    }
}
