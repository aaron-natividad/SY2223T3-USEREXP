using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageSelectManager : BaseManager
{
    [SerializeField] private StageSelectUI ui;
    [SerializeField] private ShipSpawner spawner;

    public AudioClip countdownSound;
    public List<string> sceneNames;

    private bool isCountingDown = false;

    private void Start()
    {
        selectionControls.enabled = false;
        StartCoroutine(CO_StartStageSelect());
        
    }

    public void AddScene(string sceneName)
    {
        sceneNames.Add(sceneName);
        if (sceneNames.Count >= 2 && !isCountingDown)
        {
            StartCoroutine(CO_Countdown());
        }
        else
        {
            isCountingDown = false;
            StopAllCoroutines();
        }
    }

    public void RemoveScene(string sceneName)
    {
        sceneNames.Remove(sceneName);
        if (sceneNames.Count >= 2 && !isCountingDown)
        {
            StartCoroutine(CO_Countdown());
        }
        else
        {
            isCountingDown = false;
            ui.SetPrompt("Pick A Stage");
            StopAllCoroutines();
        }
    }

    private IEnumerator CO_StartStageSelect()
    {
        yield return new WaitForSeconds(0.1f);
        cover.FadeCover(false, 0.5f);
        foreach (Ship ship in spawner.ships)
        {
            ui.SetPlayerInfo(ship);
        }
    }

    private IEnumerator CO_Countdown()
    {
        float timer;
        isCountingDown = true;

        for(int i = 3; i > 0; i--)
        {
            AudioManager.instance?.sfx.PlayOneShot(countdownSound);
            ui.SetPrompt(i);
            timer = 1f;
            while(timer > 0)
            {
                timer -=Time.deltaTime;
                yield return null;
            }
        }

        int index = Random.Range(0, sceneNames.Count);
        LoadScene(sceneNames[index]);
    }
}
