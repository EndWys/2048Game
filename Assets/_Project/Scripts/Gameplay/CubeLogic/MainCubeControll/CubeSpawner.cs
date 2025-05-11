using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface ICubeSpawner : IService
    {
        Cube SpawnMainCube();
        Cube SpawnCubeOnPosition(Vector3 position);
        void DespawnCube(Cube cube);
    }

    public class CubeSpawner : MonoBehaviour, ICubeSpawner
    {
        [SerializeField] private Transform _mainCubeSpawnPoint;
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private CubeValueSpawnConfig _spawnConfig;

        public Cube SpawnMainCube()
        {
            Cube cube = Instantiate(_cubePrefab, _mainCubeSpawnPoint.position, Quaternion.identity);
            cube.Init();
            int randomValue = _spawnConfig.GetRandomValue();
            cube.ValueHolder.SetValue(randomValue);
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