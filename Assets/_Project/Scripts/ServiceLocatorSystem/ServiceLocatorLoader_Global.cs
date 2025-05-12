using Assets._Project.Scripts.Effects;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Global : MonoBehaviour
    {
        [SerializeField] private SoundManager _soundManager;

        private ServiceLocator _global;

        public void Load()
        {
            _global = ServiceLocator.CreateGlobalServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            _soundManager.Init();
            _global.Register(_soundManager);
        }
    }
}