using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface ICubeSpawner : IService
    {
        Cube SpawnCube();
    }

    public class CubeSpawner : MonoBehaviour, ICubeSpawner
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Cube _cubePrefab;

        public Cube SpawnCube()
        {
            var cube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity);
            return cube;
        }
    }
}