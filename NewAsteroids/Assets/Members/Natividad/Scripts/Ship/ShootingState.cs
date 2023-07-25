using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        shooting.ammo = Mathf.Max(shooting.ammo - 1, 0);
        internalTimer = shooting.holdInterval;
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

        if (shooting.ammo <= 0) return;
        if (internalTimer <= 0)
        {
            bounces++;
            shooting.ammo = Mathf.Max(shooting.ammo - 1, 0);
            internalTimer = shooting.holdInterval;
        }
        else
        {
            internalTimer -= Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        shooting.SpawnProjectile(bounces);
        ship.rigidBody.AddForce(-ship.transform.up * shooting.recoilPerBounce * (bounces + 1), ForceMode2D.Impulse);
    }
}
