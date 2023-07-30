using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public string shipName;
    public delegate void ShipDeathDelegate();
    public ShipDeathDelegate OnShipDeath;

    public int playerNumber;
    [Space(10)]
    [SerializeField] private GameObject deathParticlePrefab;
    public float respawnTime;

    [Header("Movement Parameters")]
    public float moveSpeed;
    public float moveAcceleration;
    [Range(0f, 1f)] public float rotateTime;

    // Components
    [HideInInspector] public ShipShooting shooting;
    [HideInInspector] public ShipStateMachine stateMachine;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public SpriteRenderer spriteRenderer;
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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        spriteRenderer.enabled = isEnabled;
        collision.enabled = isEnabled;
        shipUI.enabled = isEnabled;
    }

    public IEnumerator CO_OnDeath()
    {
        EnableShip(false);
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        OnShipDeath?.Invoke();
        yield return new WaitForSeconds(respawnTime);
        EnableShip(true);
        yield return null;
        stateMachine.SetState(stateMachine.movingState);
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed += value;
        moveAcceleration += value;
    }
}
