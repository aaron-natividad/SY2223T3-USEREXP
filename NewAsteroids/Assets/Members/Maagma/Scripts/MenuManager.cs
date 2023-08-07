using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : BaseManager
{
    private void Start()
    {
        cover.FadeCover(false, 0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}