using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class GameUI : MonoBehaviour
{
    [Serializable]
    public struct PlayerInfoUI
    {
        public TextMeshProUGUI name;
        public Transform heartContainer;
        public List<GameObject> heartImages;
    }

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] public PlayerInfoUI[] playerInfo;
    [SerializeField] private GameObject heartImage;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void SetPlayerInfo(Ship ship)
    {
        playerInfo[ship.playerNumber].name.text = ship.name;
        for(int i = 0; i < ship.lives; i++)
        {
            GameObject hImage = Instantiate(heartImage, playerInfo[ship.playerNumber].heartContainer);
            playerInfo[ship.playerNumber].heartImages.Add(hImage);
        }
        ship.OnShipDeath += RemoveLifeUI;
    }

    public void RemoveLifeUI(int playerNumber)
    {
        if (playerInfo[playerNumber].heartImages.Count <= 0) return;

        int index = playerInfo[playerNumber].heartImages.Count - 1;
        Destroy(playerInfo[playerNumber].heartImages[index]);
        playerInfo[playerNumber].heartImages.RemoveAt(index);
    }

    public void SetEnabled(bool isEnabled)
    {
        canvas.enabled = isEnabled;
    }

    public void SetTimer(int[] timeValues)
    {
        timer.text = timeValues[0].ToString("00") + ":" + timeValues[1].ToString("00");
    }
}
