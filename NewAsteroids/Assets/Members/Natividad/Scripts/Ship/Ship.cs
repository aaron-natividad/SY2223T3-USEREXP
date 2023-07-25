using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    [SerializeField] private int playerNumber;

    [Header("Movement Parameters")]
    public float moveSpeed;
    public float moveAcceleration;
    [Range(0f, 1f)] public float rotateTime;

    // Components
    [HideInInspector] public ShipShooting shooting;
    [HideInInspector] public ShipStateMachine stateMachine;
    [HideInInspector] public Rigidbody2D rigidBody;

    // Controls
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction fire;

    private void Awake()
    {
        shooting = GetComponent<ShipShooting>();
        stateMachine = GetComponent<ShipStateMachine>();
        rigidBody = GetComponent<Rigidbody2D>();
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
}
