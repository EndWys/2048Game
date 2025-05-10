using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private static string GameSceneName = "GameScene";

    private void Start()
    {
        Application.runInBackground = true;

        if (SceneManager.loadedSceneCount == 1)
            SceneManager.LoadScene(GameSceneName, LoadSceneMode.Additive);
    }
}
