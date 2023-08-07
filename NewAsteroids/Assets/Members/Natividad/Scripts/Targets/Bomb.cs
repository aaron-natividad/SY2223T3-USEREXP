using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Target
{
    public LayerMask destroyMask;
    public GameObject explosionParticlePrefab;
    public float explosionRadius;

    public override void DoTargetBehavior()
    {
        Collider2D[] toDestroy = Physics2D.OverlapCircleAll(transform.position, explosionRadius, destroyMask);

        foreach(Collider2D destroyObject in toDestroy)
        {
            if (destroyObject.GetComponent<Ship>())
            {
                Ship ship = destroyObject.GetComponent<Ship>();
                ship.stateMachine.SetState(ship.stateMachine.deathState);
            }
        }

        AudioManager.instance?.sfx.PlayOneShot(targetSound);
        SpriteFade explosionParticle = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity).GetComponent<SpriteFade>();
        explosionParticle.Initialize(explosionRadius * 2, 0.3f);
        CameraController.instance.Shake(0.5f, 0.2f, 0.05f);
        Destroy(gameObject);
    }
}
