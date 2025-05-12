using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    public class VFXManager : IService
    {
        private VFXLibrary _vfxLiabrary;

        private VFXPool _cubeSpawnVFXPool;

        public VFXManager()
        {
            _vfxLiabrary = Resources.Load<VFXLibrary>("VFXLibrary");

            if (_vfxLiabrary == null)
            {
                Debug.LogError("VFXLibrary not found in Resources");
            }

            _cubeSpawnVFXPool = new GameObject().AddComponent<VFXPool>();
            _cubeSpawnVFXPool.SetPrefab(_vfxLiabrary.CubeSpawnEffect);
            _cubeSpawnVFXPool.CreatePool();

        }

        public void PlayCubeSpawnEffect(Vector3 position)
        {
            VFXPoolObject vfx = _cubeSpawnVFXPool.GetObject();
            vfx.Play(position);
        }
    }
}