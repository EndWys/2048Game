using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    [CreateAssetMenu(fileName = "VFXLibrary", menuName = "Effects/VFXLibrary")]
    public class VFXLibrary : ScriptableObject
    {
        [field: SerializeField] public VFXPoolObject CubeSpawnEffect { get; private set; }
    }
}