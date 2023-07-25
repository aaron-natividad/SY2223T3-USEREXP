using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public int bounces;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = transform.up.normalized * projectileSpeed;
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
                transform.up = Vector2.Reflect(transform.up, collision.contacts[0].normal);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
