using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Selection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Color activatedColor;
    [Space(10)]
    [SerializeField] private Selection selectionUp;
    [SerializeField] private Selection selectionDown;
    [SerializeField] private Selection selectionLeft;
    [SerializeField] private Selection selectionRight;
    [Space(10)]
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

    public void MoveTo(Vector2 selectDirection)
    {
        if (selectDirection.y > 0 && selectionUp != null)
        {
            SetSelected(false);
            selectionUp.SetSelected(true);
        }
        else if (selectDirection.y < 0 && selectionDown != null)
        {
            SetSelected(false);
            selectionDown.SetSelected(true);
        }
        else if (selectDirection.x < 0 && selectionLeft != null)
        {
            SetSelected(false);
            selectionLeft.SetSelected(true);
        }
        else if (selectDirection.x > 0 && selectionRight != null)
        {
            SetSelected(false);
            selectionRight.SetSelected(true);
        }
    }

    public void Activate()
    {
        StartCoroutine(CO_ActivateAnimation());
    }

    private IEnumerator CO_ActivateAnimation()
    {
        title.color = activatedColor;
        yield return new WaitForSeconds(0.1f);
        title.color = Color.white;
        yield return null;
        OnActivated?.Invoke();
    }
}
