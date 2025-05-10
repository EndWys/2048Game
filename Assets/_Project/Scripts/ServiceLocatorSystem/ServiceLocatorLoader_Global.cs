using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Global : MonoBehaviour
    {
        private ServiceLocator _global;

        public void Load()
        {
            _global = ServiceLocator.CreateGlobalServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {

        }
    }
}