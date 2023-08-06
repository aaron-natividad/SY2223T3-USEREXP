using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidStageSelect : MonoBehaviour
{
    [SerializeField] int stageNumber;
    public void DoTargetBehavior()
    {
        StageSelect();
    }

    public void StageSelect()
    {
        if (stageNumber == 0)
            SceneManager.LoadScene(1);
        else if (stageNumber == 1)
            SceneManager.LoadScene(2);
        else if (stageNumber == 2)
            SceneManager.LoadScene(3);
    }
}
