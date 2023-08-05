using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Camera cam;
    [SerializeField] private ShipSpawner spawner;
    [Space(10)]
    [SerializeField] private float minimumScale;
    [SerializeField] private float maximumScale;
    [SerializeField] private float scaleOffset;
    [Space(10)]
    [SerializeField] private float followSmoothTime;
    [SerializeField] private float zoomSmoothTime;

    private List<Ship> targets;
    private Vector3 baseCameraPosition;
    private Vector3 positionOffset;

    // Smooth Damp
    private Vector3 refVelocity;
    private float refSizeVelocity;

    private void Awake()
    {
        instance = this;
        baseCameraPosition = transform.position;
        targets = spawner.ships;
    }

    private void FixedUpdate()
    {
        baseCameraPosition = Vector3.SmoothDamp(baseCameraPosition, GetAveragePosition(), ref refVelocity, followSmoothTime);
        transform.position = baseCameraPosition + positionOffset;
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, GetMaxDistanceBetweenTargets() + scaleOffset, ref refSizeVelocity, zoomSmoothTime);
    }

    public void Shake(float magnitude, float time, float interval)
    {
        StartCoroutine(CO_Shake(magnitude, time, interval));
    }

    private float GetMaxDistanceBetweenTargets()
    {
        float distance = 0;
        if (targets.Count < 2) return minimumScale;
        
        for(int i = 0; i < targets.Count; i++)
        {
            for(int j = 0; j < targets.Count; j++)
            {
                if (targets[i] == targets[j]) continue;
                distance = Mathf.Max(Vector2.Distance(targets[i].transform.position, targets[j].transform.position), distance);
            }
        }

        distance = Mathf.Clamp(distance, minimumScale, maximumScale);
        return distance;
    }

    private Vector3 GetAveragePosition()
    {
        Vector3 averagePosition;
        float posX = 0;
        float posY = 0;

        foreach (Ship target in targets)
        {
            posX += target.transform.position.x;
            posY += target.transform.position.y;
        }

        posX /= targets.Count;
        posY /= targets.Count;
        averagePosition = new Vector3(posX, posY, transform.position.z);

        return averagePosition;
    }

    private IEnumerator CO_Shake(float magnitude, float time, float interval)
    {
        float shakeX;
        float shakeY;
        float timer = time;

        while (timer > 0)
        {
            shakeX = Random.Range(-magnitude, magnitude);
            shakeY = Random.Range(-magnitude, magnitude);
            positionOffset = new Vector3(shakeX, shakeY, 0);
            timer -= interval;
            yield return new WaitForSeconds(interval);
        }

        positionOffset = Vector3.zero;
    }
}
