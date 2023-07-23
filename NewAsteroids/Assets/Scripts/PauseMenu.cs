using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    public GameObject PausePanel;
    public GameObject StageDonePanel;

    void Start()
    {
        isPaused = false;
        PausePanel.SetActive(false);
        StageDonePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PausePanel.SetActive(true);
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }

        if(GameManager.StageCompleted)
        {
            StageCompleted();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToNextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.StageCompleted = false;
    }

    void StageCompleted()
    {
        PauseGame();
        StageDonePanel.SetActive(true);
    }
}