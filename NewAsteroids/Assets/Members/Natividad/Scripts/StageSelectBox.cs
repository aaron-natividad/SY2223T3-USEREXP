using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectBox : MonoBehaviour
{
    [SerializeField] private StageSelectManager manager;
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>())
        {
            collision.GetComponent<Ship>().shooting.canShoot = false;
            manager.AddScene(sceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>())
        {
            collision.GetComponent<Ship>().shooting.canShoot = true;
            manager.RemoveScene(sceneName);
        }
    }
}
