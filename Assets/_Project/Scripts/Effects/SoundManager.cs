using Assets._Project.Scripts.Core.Effects;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    public class SoundManager : MonoBehaviour, IService
    {
        [SerializeField] private AudioSource _audioSource;
        [Range(0,1)]
        [SerializeField] private float _volume = 0.5f;

        private SoundLibrary _soundLibrary;

        public void Init()
        {
            DontDestroyOnLoad(gameObject);

            _soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
            if (_soundLibrary == null)
            {
                Debug.LogError("SoundLibrary not found in Resources");
            }
        }

        private void PlayWithRandomPitch(AudioClip clip)
        {
            if (clip == null) return;

            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(clip, _volume);
        }


        public void PlayCubeSpawn() => PlayWithRandomPitch(_soundLibrary.CubeSpawnClip);
        public void PlayCubeMerge() => PlayWithRandomPitch(_soundLibrary.CubeMergeClip);
        public void PlayCubeLounch() => PlayWithRandomPitch(_soundLibrary.CubeLaunchClip);
        public void PlayGameOver() => PlayWithRandomPitch(_soundLibrary.GameOverClip);
    }
}