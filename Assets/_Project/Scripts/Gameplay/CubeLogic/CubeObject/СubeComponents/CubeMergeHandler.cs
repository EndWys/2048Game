using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public class CubeMergeHandler : MonoBehaviour
    {
        [SerializeField] private float _mergeImpulseThreshold = 1.5f;

        private ICubeComparer _parentCube;
        private Rigidbody _rigidbody;
        private ICubeSpawner _spawner;

        private MainCubeEventBus<MainCubeMergedEvent> _mergeEvent;

        private bool _merged;
        public bool IsMergeable => true;

        public void Init(ICubeComparer parentCube, Rigidbody rigidbody)
        {
            _parentCube = parentCube;
            _rigidbody = rigidbody;

            _spawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _mergeEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeMergedEvent>>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_merged)
                return;

            if (collision.gameObject.TryGetComponent(out CubeMergeHandler otherCube))
            {
                if (!otherCube.IsMergeable || !IsMergeable)
                    return;

                float impulse = collision.relativeVelocity.magnitude;

                if (impulse >= _mergeImpulseThreshold)
                {
                    MergeWith(otherCube);
                }
            }
        }

        private void MergeWith(CubeMergeHandler other)
        {
            if (_merged || other._merged)
                return;

            _merged = true;
            other._merged = true;

            Vector3 spawnPos = (transform.position + other.transform.position) / 2f;
            Vector3 inheritedVelocity = _rigidbody.velocity;

            Destroy(gameObject);
            Destroy(other.gameObject);

            Cube newCube = _spawner.SpawnCubeOnPosition(spawnPos);
            newCube.Init();
            newCube.MergeLaunch(inheritedVelocity);

            var provider = ServiceLocator.Local.Get<IActiveCubeProvider>();

            if (_parentCube.IsMainCube() || other._parentCube.IsMainCube())
            {
                _mergeEvent.Raise(new MainCubeMergedEvent());
            }
        }
    }
}