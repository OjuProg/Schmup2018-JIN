using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadStoryMode()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadEndlessMode()
    {
        SceneManager.LoadScene("Endless");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
