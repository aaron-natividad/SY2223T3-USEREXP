using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject CharacterSelectionPanel;
    [SerializeField] TextMeshProUGUI PlayerSelectionText;
    public GameObject[] characters;
    public int selectedCharacter = 0;

    bool isPlayer2Selecting = false;
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void BackButton()
    {
        CharacterSelectionPanel.SetActive(false);
    }

    public void StartButton()
    {
        if(isPlayer2Selecting) {
            PlayerPrefs.SetInt("secondCharacter", selectedCharacter);
            SceneManager.LoadScene("StageSelect");
        }
        else {
            PlayerSelectionText.text = "Player 2";
            PlayerPrefs.SetInt("firstCharacter", selectedCharacter);
            isPlayer2Selecting = !isPlayer2Selecting;
        }
    }
}
