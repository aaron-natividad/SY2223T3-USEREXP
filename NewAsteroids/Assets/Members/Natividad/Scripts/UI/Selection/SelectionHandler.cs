using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField] private Selection[] selections;

    private int selectionIndex = 0;

    // Controls
    [HideInInspector] public PlayerInputControls controls;
    [HideInInspector] public InputAction navigate;
    [HideInInspector] public InputAction submit;

    private bool navigatePressed = false;
    private bool submitPressed = false;

    private void Awake()
    {
        controls = new PlayerInputControls();
    }

    private void OnEnable()
    {
        navigate = controls.GenericUI.Navigate;
        navigate.Enable();
        submit = controls.GenericUI.Submit;
        submit.Enable();
    }

    private void OnDisable()
    {
        navigate.Disable();
        submit.Disable();
    }

    private void Start()
    {
        selections[0].SetSelected(true);
    }

    private void Update()
    {
        if (navigate.phase == InputActionPhase.Performed && !navigatePressed)
        {
            float direction = navigate.ReadValue<Vector2>().y;
            if (direction > 0)
            {
                MoveSelection(true);
            }
            else
            {
                MoveSelection(false);
            }
            navigatePressed = true;
        }
        else if (navigate.phase != InputActionPhase.Performed)
        {
            navigatePressed = false;
        }

        if (submit.phase == InputActionPhase.Performed && !submitPressed)
        {
            ActivateSelected();
            submitPressed = true;
        }
        else if (submit.phase != InputActionPhase.Performed)
        {
            submitPressed = false;
        }
    }

    private void MoveSelection(bool moveUpward)
    {
        if (moveUpward)
        {
            selectionIndex--;
            if (selectionIndex < 0)
            {
                selectionIndex = selections.Length - 1;
            }
        }
        else
        {
            selectionIndex++;
            if (selectionIndex > selections.Length - 1)
            {
                selectionIndex = 0;
            }
        }

        foreach (Selection s in selections)
        {
            s.SetSelected(false);
        }
        selections[selectionIndex].SetSelected(true);
    }

    private void ActivateSelected()
    {
        Debug.Log("Activated");
        foreach (Selection s in selections)
        {
            s.Activate();
        }
    }
}
