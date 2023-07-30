using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Selection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image[] arrows;

    public UnityEvent OnActivated;

    private bool isSelected;

    private void Awake()
    {
        SetSelected(false);
    }

    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        foreach (Image arrow in arrows) arrow.enabled = isSelected;
    }

    public void Activate()
    {
        if (!isSelected) return;
        StartCoroutine(CO_ActivateAnimation());
    }

    private IEnumerator CO_ActivateAnimation()
    {
        title.color = Color.red;
        foreach (Image arrow in arrows) arrow.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        title.color = Color.white;
        foreach (Image arrow in arrows) arrow.color = Color.white;
        yield return null;
        OnActivated?.Invoke();
    }
}
