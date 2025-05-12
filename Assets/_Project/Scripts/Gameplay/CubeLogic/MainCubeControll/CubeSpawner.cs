using Assets._Project.Scripts.Effects;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubePoolSystem;
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
        private const string MAIN_CUBE_LAYER = "MainCube";

        [SerializeField] private Transform _mainCubeSpawnPoint;
        [SerializeField] private CubePool _cubePool;

        private IOnFieldCubeRegister _cubeRegistry;

        private CubeValueSpawnConfig _spawnConfig;

        private VFXManager _vfxManager;

        public void Init()
        {
            GameplaySettings settings = ServiceLocator.Local.Get<GameplaySettings>();
            _spawnConfig = settings.CubeValueSpawnConfig;

            _cubeRegistry = ServiceLocator.Local.Get<OnFieldCubeRegistry>();

            _vfxManager = ServiceLocator.Global.Get<VFXManager>();

            _cubePool.CreatePool();
        }

        public Cube SpawnMainCube()
        {
            int randomValue = _spawnConfig.GetRandomValue();

            Cube cube = _cubePool.GetObject();
            cube.SetPosition(_mainCubeSpawnPoint.position);
            cube.SetRotation(Quaternion.identity);
            cube.CachedGameObject.layer = LayerMask.NameToLayer(MAIN_CUBE_LAYER);
            cube.ValueHolder.SetValue(randomValue);
            cube.Activate();

            _cubeRegistry.Register(cube);

            _vfxManager.PlayCubeSpawnEffect(_mainCubeSpawnPoint.position);

            return cube;
        }

        public Cube SpawnCubeOnPosition(Vector3 position)
        {
            Cube cube = _cubePool.GetObject();
            cube.SetPosition(position);
            cube.Activate();

            _cubeRegistry.Register(cube);

            _vfxManager.PlayCubeSpawnEffect(position);

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