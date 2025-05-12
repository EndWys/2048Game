using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    public class VFXPool : GenericObjectPool<VFXPoolObject>
    {
        private VFXPoolObject _prefab;

        protected override bool _collectionCheck => false;

        protected override int _defaultCapacity => 5;


        public void SetPrefab(VFXPoolObject prefab) => _prefab = prefab;

        protected override VFXPoolObject CratePoolObject()
        {
            VFXPoolObject poolObject = Instantiate(_prefab);
            return poolObject;
        }
    }
}