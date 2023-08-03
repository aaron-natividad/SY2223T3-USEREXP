using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCursor : MonoBehaviour
{
    [SerializeField] private Image cursorFrame;
    [SerializeField] private float moveTime;
    [SerializeField] private float popTime;

    public void MoveToPosition(Vector2 position, Vector2 size)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, position, moveTime).setEase(LeanTweenType.easeOutCubic);
        LeanTween.size(cursorFrame.rectTransform, size, moveTime).setEase(LeanTweenType.easeOutCubic);
    }

    public void Pop()
    {
        transform.localScale = Vector2.one * 1.2f;
        LeanTween.scale(gameObject, Vector2.one, popTime).setEase(LeanTweenType.easeOutCubic);
    }
}
