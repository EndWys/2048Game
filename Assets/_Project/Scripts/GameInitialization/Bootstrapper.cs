using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.GameInitialization
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string GAME_SCENE_NAME = "GameScene";

        private ServiceLocatorLoader_Global _serviceLoader = new ServiceLocatorLoader_Global();

        private void Start()
        {
            Application.runInBackground = true;

            _serviceLoader.Load();

            if (SceneManager.loadedSceneCount == 1)
                SceneManager.LoadScene(GAME_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}