using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionControls : MonoBehaviour
{
    public static event Action<PlayerInputControls> OnControlsInitialized;

    private PlayerInputControls controls;

    private void Awake()
    {
        controls = new PlayerInputControls();
    }

    private void Start()
    {
        OnControlsInitialized?.Invoke(controls);
    }

    private void OnEnable()
    {
        OnControlsInitialized?.Invoke(controls);
        controls.GenericUI.Enable();
        controls.Player1UI.Enable();
        controls.Player2UI.Enable();
    }

    private void OnDisable()
    {
        controls.GenericUI.Disable();
        controls.Player1UI.Disable();
        controls.Player2UI.Disable();
    }
}
