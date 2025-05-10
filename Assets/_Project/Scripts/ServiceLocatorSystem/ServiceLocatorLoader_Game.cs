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