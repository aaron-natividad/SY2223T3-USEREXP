using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public float moveSpeed;
    [Range(0f, 1f)] public float rotateTime;
    public float holdInterval;

    [HideInInspector] public ShipShooting shooting;
    [HideInInspector] public ShipStateMachine stateMachine;
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction fire;

    private void Awake()
    {
        shooting = GetComponent<ShipShooting>();
        stateMachine = GetComponent<ShipStateMachine>();
        controls = new PlayerInputControls();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();
        fire = controls.Player.Fire;
        fire.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
}
