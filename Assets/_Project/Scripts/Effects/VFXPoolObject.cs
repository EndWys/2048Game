using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    public class VFXPoolObject : PoolObject
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Play(Vector3 position)
        {
            CachedTrasform.position = position;
            _particleSystem.Play();
        }

        public override void OnGetFromPool()
        {
            CachedGameObject.SetActive(true);
        }


        public override void OnReleaseToPool()
        {
            CachedGameObject.SetActive(false);
        }
    }
}