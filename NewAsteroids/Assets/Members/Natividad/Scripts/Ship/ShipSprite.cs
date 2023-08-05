using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSprite : MonoBehaviour
{
    [SerializeField] private GameObject spriteParent;
    [SerializeField] private SpriteRenderer baseSprite;
    [SerializeField] private SpriteRenderer colorSprite;
    [SerializeField] private SpriteRenderer outlineSprite;
    [SerializeField] private SpriteRenderer thrusterSprite;

    public void SetEnabled(bool isEnabled)
    {
        spriteParent.SetActive(isEnabled);
    }

    public void SetColor(Color color)
    {
        colorSprite.color = color;
    }

    public void SetThrusterEnabled(bool isEnabled)
    {
        thrusterSprite.enabled = isEnabled;
    }
}
