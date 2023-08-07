using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_CharSel : BaseManager
{
    [Header("Selection Handlers")]
    [SerializeField] TestCharSel FirstPlayerSelection;
    [SerializeField] TestCharSel SecondPlayerSelection;

    private void OnEnable()
    {
        TestCharSel.OnReadyChanged += CheckPlayerReady;
    }

    private void OnDisable()
    {
        TestCharSel.OnReadyChanged -= CheckPlayerReady;
    }

    private void Start()
    {
        cover.FadeCover(false, 0.5f);
    }

    void CheckPlayerReady(AssignedPlayer player, bool isReady)
    {
        if ((FirstPlayerSelection.isReady == true) && (SecondPlayerSelection.isReady == true))
        {
            LoadScene("StageSelect");
        }
    }
}
