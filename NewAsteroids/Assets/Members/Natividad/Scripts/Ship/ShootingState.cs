using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : ShipState
{
    private int bounces;
    private float internalTimer;
    private float rotateVelocity;
    private ShipShooting shooting;

    public ShootingState(Ship ship) : base(ship)
    {
        shooting = ship.shooting;
    }

    public override void OnEnter()
    {
        bounces = 0;
        internalTimer = ship.holdInterval;
    }

    public override void OnUpdate()
    {
        if (!ship.fire.IsPressed()) stateMachine.SetState(stateMachine.movingState);

        if(ship.move.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 moveDirection = ship.move.ReadValue<Vector2>();
            float moveAngle = Vector2.SignedAngle(Vector2.up, moveDirection);
            float toAngle = Mathf.SmoothDampAngle(ship.transform.eulerAngles.z, moveAngle, ref rotateVelocity, 0.1f);
            ship.transform.rotation = Quaternion.Euler(0, 0, toAngle);
        }

        if (bounces >= shooting.maxBounces) return;
        if (internalTimer <= 0)
        {
            bounces = Mathf.Min(bounces + 1, shooting.maxBounces);
            internalTimer = ship.holdInterval;
        }
        else
        {
            internalTimer -= Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        shooting.SpawnProjectile(bounces);
    }
}
