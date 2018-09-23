using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public Animator fadeAnimator;

    private int levelToLoad;

    private void ApplyFadeEffect()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
        }
    }

    private void OnFadeComplete()
    {
        if(levelToLoad != -1)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Application.Quit();
        }
    }

    public void LoadStoryMode()
    {
        levelToLoad = 1;
        ApplyFadeEffect();
    }

    public void LoadEndlessMode()
    {
        levelToLoad = 2;
        ApplyFadeEffect();
    }

    public void LoadMainMenu()
    {
        levelToLoad = 0;
        ApplyFadeEffect();
    }

    public void QuitApplication()
    {
        levelToLoad = -1;
        ApplyFadeEffect();
    }
}
