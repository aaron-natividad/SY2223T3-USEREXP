using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Selection : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image[] images;
    [SerializeField] private TextMeshProUGUI[] texts;
    [Space(10)]
    [SerializeField] private Color activatedColor;

    [Header("Connections")]
    [SerializeField] private Selection selectionUp;
    [SerializeField] private Selection selectionDown;
    [SerializeField] private Selection selectionLeft;
    [SerializeField] private Selection selectionRight;

    [Header("Behavior")]
    public UnityEvent OnActivated;

    private SelectionGroup group;

    public void SetGroup(SelectionGroup group)
    {
        this.group = group;
    }

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            group.SetActiveSelection(this);
            group.cursor.MoveToPosition(transform.position, GetComponent<RectTransform>().sizeDelta);
        }
    }

    public bool MoveTo(Vector2 selectDirection)
    {
        if (selectDirection.y > 0 && selectionUp != null)
        {
            SetSelected(false);
            selectionUp.SetSelected(true);
            return true;
        }
        else if (selectDirection.y < 0 && selectionDown != null)
        {
            SetSelected(false);
            selectionDown.SetSelected(true);
            return true;
        }
        else if (selectDirection.x < 0 && selectionLeft != null)
        {
            SetSelected(false);
            selectionLeft.SetSelected(true);
            return true;
        }
        else if (selectDirection.x > 0 && selectionRight != null)
        {
            SetSelected(false);
            selectionRight.SetSelected(true);
            return true;
        }

        return false;
    }

    public void Activate()
    {
        StartCoroutine(CO_ActivateAnimation());
    }

    private IEnumerator CO_ActivateAnimation()
    {
        SetColor(activatedColor);
        yield return new WaitForSeconds(0.1f);
        SetColor(Color.white);
        yield return null;
        OnActivated?.Invoke();
    }

    private void SetColor(Color color)
    {
        if (images.Length > 0)
        {
            foreach (Image i in images)
                i.color = color;
        }

        if (texts.Length > 0)
        {
            foreach (TextMeshProUGUI t in texts)
                t.color = color;
        }
    }
}
