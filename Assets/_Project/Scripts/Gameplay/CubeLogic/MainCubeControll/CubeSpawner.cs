using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface ICubeSpawner : IService
    {
        Cube SpawnMainCube();
        Cube SpawnCubeOnPosition(Vector3 position);
        void DespawnCube(Cube cube);

        void DespawnAllCubes();
    }

    public class CubeSpawner : MonoBehaviour, ICubeSpawner
    {
        [SerializeField] private Transform _mainCubeSpawnPoint;
        [SerializeField] private Cube _cubePrefab;

        private IOnFieldCubeRegister _cubeRegistry;

        private CubeValueSpawnConfig _spawnConfig;

        public void Init()
        {
            _cubeRegistry = ServiceLocator.Local.Get<OnFieldCubeRegistry>();

            GameplaySettings settings = ServiceLocator.Local.Get<GameplaySettings>();
            _spawnConfig = settings.CubeValueSpawnConfig;
        }

        public Cube SpawnMainCube()
        {
            Cube cube = Instantiate(_cubePrefab, _mainCubeSpawnPoint.position, Quaternion.identity);
            cube.Init();
            int randomValue = _spawnConfig.GetRandomValue();
            cube.ValueHolder.SetValue(randomValue);

            _cubeRegistry.Register(cube);

            return cube;
        }

        public Cube SpawnCubeOnPosition(Vector3 position)
        {
            var cube = Instantiate(_cubePrefab, position, Quaternion.identity);
            _cubeRegistry.Register(cube);
            return cube;
        }

        public void DespawnCube(Cube cube)
        {
            _cubeRegistry.Unregister(cube);
            Destroy(cube.gameObject);
        }

        public void DespawnAllCubes()
        {
            Cube[] allCubes = _cubeRegistry.GetRegistryArray();

            foreach (var cube in allCubes)
            {
                DespawnCube(cube);
            }
        }
    }
}