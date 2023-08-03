using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverUI : MonoBehaviour
{
    [SerializeField] private Image cover;

    public void FadeCover(bool fadeIn, float fadeTime)
    {
        StartCoroutine(CO_FadeCover(fadeIn, fadeTime));
    }

    private IEnumerator CO_FadeCover(bool fadeIn, float fadeTime)
    {
        Color fadeColor = fadeIn ? Color.black : Color.clear;
        LeanTween.color(cover.rectTransform, fadeColor, fadeTime);
        yield return new WaitForSeconds(fadeTime);
    }
}
