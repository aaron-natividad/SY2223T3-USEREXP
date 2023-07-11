using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public ShipMovement movement;
    [HideInInspector] public ShipShooting shooting;

    [SerializeField] private GameObject deathParticle;
    [SerializeField] private float respawnTime;

    public int points = 0;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<ShipMovement>();
        shooting = GetComponent<ShipShooting>();
    }

    public IEnumerator CO_TakeDamage()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        rigidBody.velocity = Vector3.zero;
        spriteRenderer.enabled = false;
        movement.enabled = false;
        shooting.enabled = false;

        yield return new WaitForSeconds(respawnTime);
        spriteRenderer.enabled = true;
        movement.enabled = true;
        shooting.enabled = true;
    }
}
