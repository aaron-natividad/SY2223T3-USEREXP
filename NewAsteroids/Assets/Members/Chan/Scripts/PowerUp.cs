using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D col;
    [SerializeField] private bool respawnable;
    [SerializeField] private float respawnTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ShipSpecial>())
        {
            collision.GetComponent<ShipSpecial>().SetCanActivate(true);

            if (respawnable)
                StartCoroutine(CO_RespawnPowerup());
            else
                Destroy(gameObject);
        }
    }

    private IEnumerator CO_RespawnPowerup()
    {
        col.enabled = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        col.enabled = true;
        spriteRenderer.enabled = true;
    }
}
