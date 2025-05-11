using Assets._Project.Scripts.Utilities;

namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public abstract class PoolObject : CachedMonoBehaviour, IPoolObject
    {
        public abstract void OnGetFromPool();

        public abstract void OnReleaseToPool();
    }
}