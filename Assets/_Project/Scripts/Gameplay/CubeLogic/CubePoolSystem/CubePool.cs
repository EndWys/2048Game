using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubePoolSystem
{
    public class CubePool : GenericObjectPool<Cube>
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _defaultSpawnPoint;

        [SerializeField] private Cube _cubePrefab;

        protected override bool _collectionCheck => false;
        protected override int _defaultCapacity => 30;

        protected override Cube CratePoolObject()
        {
            var cube = Instantiate(_cubePrefab, _defaultSpawnPoint.position, Quaternion.identity, _parent);
            cube.Init();
            return cube;
        }

        protected override void OnGetObjectFromPool(Cube poolObject)
        {
            poolObject.CachedTrasform.position = _defaultSpawnPoint.position;
            base.OnGetObjectFromPool(poolObject);
        }
    }
}