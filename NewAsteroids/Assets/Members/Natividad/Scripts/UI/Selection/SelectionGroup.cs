using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionGroup : MonoBehaviour
{
    [SerializeField] private AssignedPlayerUI assignedPlayer;
    public SelectionCursor cursor;

    [SerializeField] private bool lockCursorOnSelect;

    [SerializeField] private Selection activeSelection;

    private bool isEnabled = true;

    private void OnEnable()
    {
        SelectionControls.OnControlsInitialized += SetControls;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Selection>().SetGroup(this);
        }
        transform.GetChild(0).GetComponent<Selection>().SetSelected(true);
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
        activeSelection?.MoveTo(context.ReadValue<Vector2>());
    }

    public void ActivateSelection(InputAction.CallbackContext context)
    {
        if (!isEnabled) return;
        cursor.Pop();
        activeSelection?.Activate();
        if (lockCursorOnSelect) DisableGroup();
    }

    public void CancelSelection(InputAction.CallbackContext context)
    {
        if (isEnabled) return;
        EnableGroup();
    }
}
