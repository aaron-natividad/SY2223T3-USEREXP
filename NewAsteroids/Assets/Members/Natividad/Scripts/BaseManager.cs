using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    public static BaseManager instance;

    [Header("Base Manager Components")]
    public SelectionControls selectionControls;
    public CoverUI cover;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(CO_LoadScene(sceneName));
    }

    private IEnumerator CO_LoadScene(string sceneName)
    {
        cover.FadeCover(true, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
