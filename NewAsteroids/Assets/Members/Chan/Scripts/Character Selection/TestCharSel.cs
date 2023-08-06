using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharSel : MonoBehaviour
{
    [SerializeField] GameObject redShipPanel;
    [SerializeField] GameObject blueShipPanel;
    [SerializeField] GameObject whiteShipPanel;
    [SerializeField] int PlayerNumber;

    bool isRedOn = false;
    bool isBlueOn = false;
    bool isWhiteOn = false;

    int selectedCharacter;

    public bool isReady;

    void Start()
    {
        redShipPanel.SetActive(false);
        blueShipPanel.SetActive(false);
        whiteShipPanel.SetActive(false);

        isRedOn = false;
        isBlueOn = false;
        isWhiteOn = false;
        selectedCharacter = 0;
    }

    void ReadyUp()
    {
        isReady = true;

        // disable movement 
        // press K or numpad 2 to cancel ready
        // call NotReady() which will make isReady off and enable movement
    }

    public void SelectRed()
    {
        isBlueOn = false;
        isWhiteOn = false;

        if (!isRedOn) {
        blueShipPanel.SetActive(false);
        whiteShipPanel.SetActive(false);

        redShipPanel.SetActive(true);
        isRedOn = true;
        }
        else if (isRedOn) {
            if (PlayerNumber == 1) {
                selectedCharacter = 1;
                PlayerPrefs.SetInt("firstCharacter", selectedCharacter);
                ReadyUp();
            }
            else if (PlayerNumber == 2) {
                selectedCharacter = 1;
                PlayerPrefs.SetInt("secondCharacter", selectedCharacter);
                ReadyUp();
            }
        }
    }

    public void SelectBlue()
    {
        isRedOn = false;
        isWhiteOn = false;

        if (!isBlueOn) {
        redShipPanel.SetActive(false);
        whiteShipPanel.SetActive(false);

        blueShipPanel.SetActive(true);
        isBlueOn = true;
        }
        else if (isBlueOn) {
             if (PlayerNumber == 1) {
                selectedCharacter = 2;
                PlayerPrefs.SetInt("firstCharacter", selectedCharacter);
                ReadyUp();
            }
            else if (PlayerNumber == 2) {
                selectedCharacter = 2;
                PlayerPrefs.SetInt("secondCharacter", selectedCharacter);
                ReadyUp();
            }
        }
    }
    
    public void SelectWhite()
    {
        isBlueOn = false;
        isRedOn = false;

        if (!isWhiteOn) {
        blueShipPanel.SetActive(false);
        redShipPanel.SetActive(false);

        whiteShipPanel.SetActive(true);
        isWhiteOn = true;
        }
        else if (isWhiteOn) {
             if (PlayerNumber == 1) {
                selectedCharacter = 3;
                PlayerPrefs.SetInt("firstCharacter", selectedCharacter);
                ReadyUp();
            }
            else if (PlayerNumber == 2) {
                selectedCharacter = 3;
                PlayerPrefs.SetInt("secondCharacter", selectedCharacter);
                ReadyUp();
            }
        }
    }
}
