using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionGroup : MonoBehaviour
{
    [SerializeField] private AssignedPlayerUI assignedPlayer;
    public SelectionCursor cursor;

    [SerializeField] private Selection activeSelection;

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
        Debug.Log("Assigned");
        switch (assignedPlayer)
        {
            case AssignedPlayerUI.Player1:
                controls.Player1UI.Navigate.performed += MoveActiveSelection;
                controls.Player1UI.Submit.performed += ActivateSelection;
                break;
            case AssignedPlayerUI.Player2:
                controls.Player2UI.Navigate.performed += MoveActiveSelection;
                controls.Player2UI.Submit.performed += ActivateSelection;
                break;
            default:
                controls.GenericUI.Navigate.performed += MoveActiveSelection;
                controls.GenericUI.Submit.performed += ActivateSelection;
                break;
        }
    }

    public void SetActiveSelection(Selection selection)
    {
        activeSelection = selection;
    }

    public void MoveActiveSelection(InputAction.CallbackContext context)
    {
        activeSelection?.MoveTo(context.ReadValue<Vector2>());
    }

    public void ActivateSelection(InputAction.CallbackContext context)
    {
        cursor.Pop();
        activeSelection?.Activate();
    }
}
