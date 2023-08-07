using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharSel : MonoBehaviour
{
    public static event Action<AssignedPlayer, bool> OnReadyChanged;

    [SerializeField] AssignedPlayer assignedPlayer;
    [SerializeField] private SelectionGroup selectionGroup;
    [SerializeField] private CharacterSelectionPanel panel;

    private string[] assignedPlayerPref = { "firstCharacter", "secondCharacter" };
    public bool isReady;

    private void OnEnable()
    {
        selectionGroup.OnSelectionCancelled += CancelSelection;
    }

    private void OnDisable()
    {
        selectionGroup.OnSelectionCancelled -= CancelSelection;
    }

    public void SetReady(bool isReady)
    {
        this.isReady = isReady;
        panel.SetReady(isReady);
        OnReadyChanged?.Invoke(assignedPlayer, isReady);
    }

    public void LockInSelection(int selectedCharacter)
    {
        PlayerPrefs.SetInt(assignedPlayerPref[(int)assignedPlayer], selectedCharacter);
        SetReady(true);
    }

    public void CancelSelection()
    {
        SetReady(false);
    }
}
