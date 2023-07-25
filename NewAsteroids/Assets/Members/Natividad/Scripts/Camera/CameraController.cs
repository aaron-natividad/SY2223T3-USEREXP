using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Space(10)]
    [SerializeField] private float minimumScale;
    [SerializeField] private float maximumScale;
    [Space(10)]
    [SerializeField] private float followSmoothTime;
    [SerializeField] private float zoomSmoothTime;
    [Space(10)]
    [SerializeField] private List<GameObject> targets;

    private Vector3 refVelocity;
    private float refSizeVelocity;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetAveragePosition(), ref refVelocity, followSmoothTime);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, GetMaxDistanceBetweenTargets(), ref refSizeVelocity, zoomSmoothTime);
    }

    public float GetMaxDistanceBetweenTargets()
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

    public Vector3 GetAveragePosition()
    {
        Vector3 averagePosition;
        float posX = 0;
        float posY = 0;

        foreach (GameObject target in targets)
        {
            posX += target.transform.position.x;
            posY += target.transform.position.y;
        }

        posX /= targets.Count;
        posY /= targets.Count;
        averagePosition = new Vector3(posX, posY, transform.position.z);

        return averagePosition;
    }
}
