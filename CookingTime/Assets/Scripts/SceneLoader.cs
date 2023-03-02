using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}