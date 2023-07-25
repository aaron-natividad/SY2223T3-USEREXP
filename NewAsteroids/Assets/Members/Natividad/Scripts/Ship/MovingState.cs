using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : ShipState
{
    private float rotateVelocity;

    public MovingState(Ship ship) : base(ship) { }

    public override void OnEnter()
    {
       
    }

    public override void OnUpdate()
    {
        if (ship.fire.IsPressed()) stateMachine.SetState(stateMachine.shootingState);

        if (ship.move.ReadValue<Vector2>() == Vector2.zero) return;

        Vector2 moveDirection = ship.move.ReadValue<Vector2>();
        float moveAngle = Vector2.SignedAngle(Vector2.up, moveDirection);
        float toAngle = Mathf.SmoothDampAngle(ship.transform.eulerAngles.z, moveAngle, ref rotateVelocity, 0.1f);
        ship.transform.rotation = Quaternion.Euler(0, 0, toAngle);
        ship.rigidBody.velocity = ship.transform.up * ship.moveSpeed;
    }
}
