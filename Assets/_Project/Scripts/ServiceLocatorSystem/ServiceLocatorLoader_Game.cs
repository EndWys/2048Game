using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.GameplayInput;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Game : MonoBehaviour
    {
        private ServiceLocator _local;

        public void Load()
        {
            _local = ServiceLocator.CreateLocalSceneServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            _local.Register<IInputHandler>(new InputHandler());

            _local.Register(new GameManager()).Init();
        }

        private void OnDestroy()
        {
            UnregisterAllServices();
        }

        private void UnregisterAllServices()
        {
            if (_local == null)
                return;

            _local.Unregister<IInputHandler>();
            _local.Unregister<GameManager>();
        }
    }
}