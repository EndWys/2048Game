using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.GameInitialization
{
    public class GameSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceLocatorLoader_Game _serviceLoader;
        private void Start()
        {
            _serviceLoader.Load();
        }
    }
}