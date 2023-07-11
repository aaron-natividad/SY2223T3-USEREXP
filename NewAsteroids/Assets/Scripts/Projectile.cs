using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Ship,
    Target,
    Bounced
}

public class Projectile : MonoBehaviour
{
    public GameObject bounceParticle;
    public ProjectileType type;

    // Colors are stored in all projectiles so we can change the type of any projectile in realtime
    [Header("Colors")]
    public Color shipProjectileColor;
    public Color targetProjectileColor;
    public Color bouncedProjectileColor;

    [Space(10)]
    public ProjectileDetection detection;
    public float projectileSpeed;
    public int bounces;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeType(type);
        Destroy(gameObject, 5f); //temporary
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.up * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            bounces--;
            if (bounces <= 0)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instantiate(bounceParticle, transform.position, Quaternion.identity);
                ChangeType(ProjectileType.Bounced);
                transform.up = Vector2.Reflect(transform.up, collision.contacts[0].normal);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(bounceParticle, transform.position, Quaternion.identity);
    }

    public void ChangeType(ProjectileType newType)
    {
        type = newType;
        switch (newType)
        {
            case ProjectileType.Ship:
                spriteRenderer.color = shipProjectileColor;
                break;
            case ProjectileType.Target:
                spriteRenderer.color = targetProjectileColor;
                break;
            case ProjectileType.Bounced:
                spriteRenderer.color = bouncedProjectileColor;
                break;
        }
    }
}
