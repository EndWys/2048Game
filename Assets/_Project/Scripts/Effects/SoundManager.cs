using Assets._Project.Scripts.Core.Effects;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Effects
{
    public class SoundManager : IService
    {
        private SoundLibrary _soundLibrary;
        private AudioSource _audioSource;

        private float _volume = 0.3f;

        public SoundManager()
        {
            _soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
            if (_soundLibrary == null)
            {
                Debug.LogError("SoundLibrary not found in Resources");
            }

            GameObject audioSourceObject = new GameObject();

            _audioSource = audioSourceObject.AddComponent<AudioSource>();
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