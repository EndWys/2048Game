using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private ServiceLocatorLoader_Game _serviceLoader;
    private void Start()
    {
        _serviceLoader.Load();
    }
}
