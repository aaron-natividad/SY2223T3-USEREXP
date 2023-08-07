using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPanel : MonoBehaviour
{
    [Serializable]
    public struct CharacterSelectionShip
    {
        public string name;
        public Sprite baseSprite;
        public Sprite colorSprite;
        public Sprite outlineSprite;
        [TextArea] public string powerupDescription;
    }

    public SelectionGroup selectionGroup;
    public CharacterSelectionShip[] ships;

    [Header("UI Components")]
    public Image panel;
    public TextMeshProUGUI shipName;
    public Image shipBase;
    public Image shipColor;
    public Image shipOutline;
    public TextMeshProUGUI shipPowerupTitle;
    public TextMeshProUGUI shipPowerupDescription;
    public TextMeshProUGUI readyText;

    [Header("Parameters")]
    public int startingIndex;
    public string namePrefix;
    public Color playerColor;

    private Color defaultPanelColor;
    private Color readyPanelColor;

    private void OnEnable()
    {
        selectionGroup.OnSelectionMoved += SetShip;
    }

    private void OnDisable()
    {
        selectionGroup.OnSelectionMoved -= SetShip;
    }

    private void Start()
    {
        defaultPanelColor = panel.color;
        readyPanelColor = defaultPanelColor;
        readyPanelColor.r -= 0.3f;
        readyPanelColor.g -= 0.3f;
        readyPanelColor.b -= 0.3f;
        SetShip(startingIndex);
    }

    public void SetShip(int index)
    {
        shipName.text = namePrefix + " " + ships[index].name;
        shipBase.sprite = ships[index].baseSprite;
        shipColor.sprite = ships[index].colorSprite;
        shipOutline.sprite = ships[index].outlineSprite;
        shipPowerupDescription.text = ships[index].powerupDescription;

        shipColor.color = playerColor;
    }

    public void SetReady(bool isReady)
    {
        panel.color = isReady ? readyPanelColor : defaultPanelColor;
        shipPowerupTitle.enabled = !isReady;
        shipPowerupDescription.enabled = !isReady;
        readyText.enabled = isReady;
    }
}
