using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnDayEndedState += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        GameManager.OnDayEndedState -= LoadMainMenuScene;
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}