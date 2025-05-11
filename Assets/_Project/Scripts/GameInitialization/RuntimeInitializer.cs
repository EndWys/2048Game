using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.GameInitialization
{
    public class RuntimeInitializer
    {
        private const string BOOTSTRAPPER_SCENE_NAME = "Bootstrapper";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
#if UNITY_EDITOR
            var currentlyLoadedEditorScene = SceneManager.GetActiveScene();
#endif

            if (SceneManager.GetSceneByName(BOOTSTRAPPER_SCENE_NAME).isLoaded != true)
                SceneManager.LoadScene(BOOTSTRAPPER_SCENE_NAME);

#if UNITY_EDITOR
            if (currentlyLoadedEditorScene.IsValid() && currentlyLoadedEditorScene.name != BOOTSTRAPPER_SCENE_NAME)
                SceneManager.LoadSceneAsync(currentlyLoadedEditorScene.name, LoadSceneMode.Additive);
#endif
        }
    }
}