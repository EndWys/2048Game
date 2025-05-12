using UnityEngine;

namespace Assets._Project.Scripts.Core.Effects
{
    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/SoundLibrary")]
    public class SoundLibrary : ScriptableObject
    {
        [field: SerializeField] public AudioClip CubeSpawnClip { get; private set; }
        [field: SerializeField] public AudioClip CubeMergeClip { get; private set; }
        [field: SerializeField] public AudioClip CubeLaunchClip { get; private set; }
        [field: SerializeField] public AudioClip GameOverClip { get; private set; }
    }
}