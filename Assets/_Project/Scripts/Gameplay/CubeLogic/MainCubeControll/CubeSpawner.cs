using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.UIElements;

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
        private const string MAIN_CUBE_LAYER = "MainCube";

        [SerializeField] private Transform _mainCubeSpawnPoint;
        [SerializeField] private CubePool _cubePool;

        private IOnFieldCubeRegister _cubeRegistry;

        private CubeValueSpawnConfig _spawnConfig;

        public void Init()
        {
            GameplaySettings settings = ServiceLocator.Local.Get<GameplaySettings>();
            _spawnConfig = settings.CubeValueSpawnConfig;

            _cubeRegistry = ServiceLocator.Local.Get<OnFieldCubeRegistry>();

            _cubePool.CreatePool();
        }

        public Cube SpawnMainCube()
        {
            int randomValue = _spawnConfig.GetRandomValue();

            Cube cube = _cubePool.GetObject();

            _cubeRegistry.Register(cube);

            cube.SetPosition(_mainCubeSpawnPoint.position);
            cube.SetRotation(Quaternion.identity);
            cube.CachedGameObject.layer = LayerMask.NameToLayer(MAIN_CUBE_LAYER);
            cube.ValueHolder.SetValue(randomValue);

            return cube;
        }

        public Cube SpawnCubeOnPosition(Vector3 position)
        {
            Cube cube = _cubePool.GetObject();

            _cubeRegistry.Register(cube);

            cube.SetPosition(position);

            return cube;
        }

        public void DespawnAllCubes()
        {
            Cube[] allCubes = _cubeRegistry.GetRegistryArray();

            foreach (var cube in allCubes)
            {
                DespawnCube(cube);
            }
        }

        public void DespawnCube(Cube cube)
        {
            _cubeRegistry.Unregister(cube);
            _cubePool.ReleaseObject(cube);
        }
    }
}