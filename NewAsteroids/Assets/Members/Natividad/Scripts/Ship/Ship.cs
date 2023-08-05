using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    [Header("Ship Info")]
    public string shipName;
    public AssignedPlayer assignedPlayer;

    [Header("Movement")]
    public float moveSpeed;
    public float moveAcceleration;
    [Range(0f, 1f)] public float rotateTime;

    // Components
    [HideInInspector] public ShipHealth health;
    [HideInInspector] public ShipShooting shooting;
    [HideInInspector] public ShipSpecial special;
    [HideInInspector] public ShipStateMachine stateMachine;

    [HideInInspector] public ShipUI shipUI;
    [HideInInspector] public ShipSprite shipSprite;

    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Collider2D collision;

    // Controls
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction fireAction;
    [HideInInspector] public InputAction specialAction;

    private void Awake()
    {
        health = GetComponent<ShipHealth>();
        shooting = GetComponent<ShipShooting>();
        special = GetComponent<ShipSpecial>();
        stateMachine = GetComponent<ShipStateMachine>();
        
        rigidBody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();

        shipUI = GetComponentInChildren<ShipUI>();
        shipSprite = GetComponentInChildren<ShipSprite>();

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
        shipSprite.SetColor(color);
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
        shipUI.gameObject.SetActive(isEnabled);
        shipSprite.SetEnabled(isEnabled);
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
