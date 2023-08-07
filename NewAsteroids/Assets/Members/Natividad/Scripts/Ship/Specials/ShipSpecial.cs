using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpecial : MonoBehaviour
{
    public event Action<bool, int> OnCanActivateChanged;

    [SerializeField] protected AudioClip activateSound;
    [SerializeField] protected Ship ship;
    [SerializeField] protected float activeTime;
    [Space(10)]
    public Sprite specialIcon;
    public bool canActivate;
    public bool isActive;

    protected float internalTimer;

    private void Start()
    {
        SetCanActivate(false);
    }

    public void SetCanActivate(bool canActivate)
    {
        this.canActivate = canActivate;
        OnCanActivateChanged?.Invoke(canActivate, (int)ship.assignedPlayer);
    }

    public virtual void Activate()
    {
        
    }
}
