using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface ICubeSpawner : IService
    {
        Cube SpawnCube();
        Cube SpawnCubeOnPosition(Vector3 position);
        void DespawnCube(Cube cube);
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

        public Cube SpawnCubeOnPosition(Vector3 position)
        {
            var cube = Instantiate(_cubePrefab, position, Quaternion.identity);
            return cube;
        }

        public void DespawnCube(Cube cube)
        {
            Destroy(cube.gameObject);
        }
    }
}