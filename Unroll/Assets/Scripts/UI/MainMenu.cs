using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public LoadZone load;

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
