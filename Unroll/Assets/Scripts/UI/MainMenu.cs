using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public LoadZone load;

    private void Start()
    {
        load.LoadGame();
    }

    public void PlayGame()
    {
        load.LoadGame();
        SceneManager.LoadScene(load.GetLevelToLoad());
    }

    public void QuitGame()
    {
        Debug.Log("Quit...");
        Application.Quit();
    }

}
