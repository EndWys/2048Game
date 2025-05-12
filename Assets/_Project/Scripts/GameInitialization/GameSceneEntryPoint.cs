using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.GameplayInput;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.GameInitialization
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceLocatorLoader_Game _serviceLoader;

        private InputUpdater _inputUpdater;
        private GameManager _gameManager;

        private bool _initialized;
        private void Start()
        {
            _serviceLoader.Load();

            _inputUpdater = new InputUpdater().Init();

            _gameManager = ServiceLocator.Local.Get<GameManager>();
            _gameManager.StartGame();

            _initialized = true;
        }

        private void Update()
        {
            _inputUpdater.Update();

            _gameManager.Tick();
        }

        private void OnDestroy()
        {
            if (_initialized)
                _gameManager.ExitGame();
        }
    }
}