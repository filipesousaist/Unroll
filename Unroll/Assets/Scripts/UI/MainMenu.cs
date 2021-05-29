using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
#if UNITY_ENGINE
        Application.Quit();
#elif UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

}
