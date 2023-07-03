using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Thrust") == 1)
        {
            rigidBody.velocity = transform.up * moveSpeed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.Rotate(new Vector3(0,0, -Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime));
        }
    }
}
