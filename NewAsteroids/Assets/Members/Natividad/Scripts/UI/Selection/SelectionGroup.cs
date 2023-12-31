using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class SelectionGroup : MonoBehaviour
{
    public event Action<int> OnSelectionMoved;
    public event Action OnSelectionCancelled;

    [SerializeField] private AssignedPlayerUI assignedPlayer;
    [SerializeField] public SelectionCursor cursor;
    [Space(10)]
    [SerializeField] private int startingSelection = 0;
    [SerializeField] private bool lockCursorOnSelect;
    [Space(10)]
    [SerializeField] private AudioClip activateSound;
    [SerializeField] private AudioClip cancelSound;
    
    private Selection activeSelection;
    private List<Selection> selections = new List<Selection>();
    private bool isEnabled = true;

    private void OnEnable()
    {
        SelectionControls.OnControlsInitialized += SetControls;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Selection>().SetGroup(this);
            selections.Add(transform.GetChild(i).GetComponent<Selection>());
        }
        transform.GetChild(startingSelection).GetComponent<Selection>().SetSelected(true);
    }

    private void OnDisable()
    {
        SelectionControls.OnControlsInitialized -= SetControls;
    }

    private void SetControls(PlayerInputControls controls)
    {
        switch (assignedPlayer)
        {
            case AssignedPlayerUI.Player1:
                controls.Player1UI.Navigate.performed += MoveActiveSelection;
                controls.Player1UI.Submit.performed += ActivateSelection;
                controls.Player1UI.Cancel.performed += CancelSelection;
                break;
            case AssignedPlayerUI.Player2:
                controls.Player2UI.Navigate.performed += MoveActiveSelection;
                controls.Player2UI.Submit.performed += ActivateSelection;
                controls.Player2UI.Cancel.performed += CancelSelection;
                break;
            default:
                controls.GenericUI.Navigate.performed += MoveActiveSelection;
                controls.GenericUI.Submit.performed += ActivateSelection;
                controls.GenericUI.Cancel.performed += CancelSelection;
                break;
        }
    }

    public void EnableGroup()
    {
        isEnabled = true;
    }

    public void DisableGroup()
    {
        isEnabled = false;
    }

    public void SetActiveSelection(Selection selection)
    {
        activeSelection = selection;
    }

    public void MoveActiveSelection(InputAction.CallbackContext context)
    {
        if (!isEnabled) return;
        if (activeSelection != null)
        {
            if (activeSelection.MoveTo(context.ReadValue<Vector2>()))
                OnSelectionMoved?.Invoke(selections.IndexOf(activeSelection));
        }
    }

    public void ActivateSelection(InputAction.CallbackContext context)
    {
        if (!isEnabled) return;
        AudioManager.instance?.sfx.PlayOneShot(activateSound);
        cursor.Pop();
        activeSelection?.Activate();
        if (lockCursorOnSelect) DisableGroup();
    }

    public void CancelSelection(InputAction.CallbackContext context)
    {
        if (isEnabled) return;
        AudioManager.instance?.sfx.PlayOneShot(cancelSound);
        OnSelectionCancelled?.Invoke();
        EnableGroup();
    }
}
