using Assets._Project.Scripts.Utilities;

namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public abstract class PoolObject : CachedMonoBehaviour, IPoolObject
    {
        public virtual void OnGetFromPool() { }

        public abstract void OnReleaseToPool();
    }
}