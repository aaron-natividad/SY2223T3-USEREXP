using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image specialBase;
    [SerializeField] private Image specialIcon;
    [SerializeField] private Transform heartContainer;

    [Header("Prefab")]
    [SerializeField] private GameObject heartImagePrefab;

    [Header("Colors")]
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;

    private List<GameObject> heartImages = new List<GameObject>();
    public void SetPlayerInfo(Ship ship)
    {
        specialIcon.sprite = ship.special.specialIcon;
        for (int i = 0; i < ship.health.GetLives(); i++)
        {
            GameObject hImage = Instantiate(heartImagePrefab, heartContainer);
            heartImages.Add(hImage);
        }
        ship.special.OnCanActivateChanged += SetSpecialUIActivated;
        ship.health.OnShipDeath += RemoveLifeUI;
    }

    public void SetSpecialUIActivated(bool isActivated, int playerNumber)
    {
        specialIcon.color = isActivated ? activeColor : inactiveColor;
        specialBase.color = isActivated ? activeColor : inactiveColor;
    }

    public void RemoveLifeUI(int playerNumber)
    {
        if (heartImages.Count <= 0) return;

        int index = heartImages.Count - 1;
        Destroy(heartImages[index]);
        heartImages.RemoveAt(index);
    }
}
