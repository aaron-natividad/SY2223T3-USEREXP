using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_CharSel : MonoBehaviour
{
    [SerializeField] TestCharSel FirstPlayerSelection;
    [SerializeField] TestCharSel SecondPlayerSelection;

    void Start()
    {
        InvokeRepeating("CheckPlayerReady", 3f, 2.5f);
    }

    void CheckPlayerReady()
    {
        if ((FirstPlayerSelection.isReady == true) && (SecondPlayerSelection.isReady == true))
        {
            // start game if both are ready
            SceneManager.LoadScene("StageSelect");
        }
    }
}
