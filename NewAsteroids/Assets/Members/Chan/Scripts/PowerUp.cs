using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject _sprite;
    [SerializeField] BoxCollider2D _collider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Ship player = other.GetComponent<Ship>();

            if (player.shipName == "red")
                StartCoroutine(RedPowerUp(player));
            else if(player.shipName == "blue")
                StartCoroutine(BluePowerUp(player));
            else if(player.shipName == "white")
                StartCoroutine(WhitePowerUp(player));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _sprite.SetActive(false);
            _collider.enabled = false;
        }
    }

    public IEnumerator RedPowerUp(Ship playerShip)
    {
        playerShip.shooting.maxAmmo = 9999;
        playerShip.shooting.ammo = 9999;
        yield return new WaitForSeconds(5f);

        playerShip.shooting.maxAmmo = 3;
        playerShip.shooting.ammo = 0;

        // Destroy power up after the effects revert
        Destroy(this);
    }

    public IEnumerator BluePowerUp(Ship playerShip)
    {
        playerShip.collision.enabled = false;
        yield return new WaitForSeconds(5f);

        playerShip.collision.enabled = true;

        // Destroy power up after the effects revert
        Destroy(this);
    }

    public IEnumerator WhitePowerUp(Ship playerShip)
    {
        playerShip.SetMoveSpeed(5);
        yield return new WaitForSeconds(5f);

        playerShip.SetMoveSpeed(-5);

        // Destroy power up after the effects revert
        Destroy(this);
    }
}
