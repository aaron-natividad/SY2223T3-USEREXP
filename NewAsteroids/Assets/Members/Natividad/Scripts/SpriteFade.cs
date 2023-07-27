using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(float size, float fadeTime)
    {
        transform.localScale = Vector3.one * size;
        spriteRenderer.color = Color.white;
        LeanTween.color(gameObject, Color.clear, fadeTime);
        Destroy(gameObject, fadeTime);
    }
}
