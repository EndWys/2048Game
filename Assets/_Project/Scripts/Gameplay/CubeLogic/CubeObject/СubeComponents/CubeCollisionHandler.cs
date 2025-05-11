using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public class CubeCollisionHandler : MonoBehaviour
    {
        [SerializeField] private float _mergeImpulseThreshold = 1.5f;

        public event Action<Cube> OnCubeCollide;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out Cube otherCube))
            {
                float impulse = collision.relativeVelocity.magnitude;

                if (impulse >= _mergeImpulseThreshold)
                {
                    OnCubeCollide?.Invoke(otherCube);
                }
            }
        }
    }
}