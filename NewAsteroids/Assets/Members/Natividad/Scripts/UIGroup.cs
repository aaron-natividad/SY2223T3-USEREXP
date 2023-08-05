using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    public static event Action<UIGroup> OnGroupActivated;

    [Header("UI Group")]
    [SerializeField] protected Canvas canvas;

    private void OnEnable()
    {
        OnGroupActivated += SetEnabled;
    }

    private void OnDisable()
    {
        OnGroupActivated -= SetEnabled;
    }

    public virtual void Activate()
    {
        OnGroupActivated?.Invoke(this);
    }

    protected virtual void SetEnabled(UIGroup activatedGroup)
    {
        canvas.enabled = this == activatedGroup;
    }
}
