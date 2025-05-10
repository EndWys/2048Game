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
        private void Start()
        {
            _serviceLoader.Load();

            _inputUpdater = new InputUpdater().Init();
        }

        private void Update()
        {
            _inputUpdater.Update();

            ServiceLocator.Local.Get<GameManager>().Tick();
        }
    }
}