using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    bool isDuo;
    int SelectedLevel;
    TMP_Dropdown StageSelector;
    public GameObject SettingsMenu;
    public GameObject CharacterSelectionPanel;

    void Start()
    {   
        StageSelector = FindObjectOfType<TMP_Dropdown>();
        isDuo = false;   
        SettingsMenu.SetActive(false);
        CharacterSelectionPanel.SetActive(false);
    }

    void Update()
    {
        SelectedLevel = StageSelector.value;
    }

    public void StartGame()
    {
        CharacterSelectionPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Buttons On Clicked
    public void SettingsOnClicked()
    {
        SettingsMenu.SetActive(true);
    }

    public void SoloOnClicked()
    {
        isDuo = false;
    }

    public void DuoOnClicked()
    {
        isDuo = true;
    }
    
    public void TutorialOnClicked()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void BackOnClicked()
    {
        SettingsMenu.SetActive(false);
    }
}