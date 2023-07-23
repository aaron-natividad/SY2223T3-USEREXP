using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static float timeLimit = 5;
    public static float timer;
    public static bool StageCompleted;
    
    void Update()
    {
        timer = Time.timeSinceLevelLoad % 60;
        if(timer >= timeLimit)
        {
            StageCompleted = true;   
        }
    }
}