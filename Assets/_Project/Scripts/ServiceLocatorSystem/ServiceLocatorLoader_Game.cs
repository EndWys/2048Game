using Assets._Project.Scripts.Gameplay.GameManagment;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Game : MonoBehaviour
    {
        [SerializeField] GameManager _gameManager;

        private ServiceLocator _local;

        public void Load()
        {
            _local = ServiceLocator.CreateLocalSceneServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            _local.Register(_gameManager).Init();
        }

        private void OnDestroy()
        {
            UnregisterAllServices();
        }

        private void UnregisterAllServices()
        {

        }
    }
}