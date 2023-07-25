using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameObject bounceParticle;
    [SerializeField] private float projectileSpeed;

    private int bounces = 0;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Initialize(int bounces)
    {
        this.bounces = bounces;
        rigidBody.velocity = transform.up * projectileSpeed;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.up * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (bounces <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                bounces--;
                Instantiate(bounceParticle, transform.position, Quaternion.identity);
                transform.up = Vector2.Reflect(transform.up, collision.contacts[0].normal);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(bounceParticle, transform.position, Quaternion.identity);
    }
}
