using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public interface ILaunching
    {
        bool IsLaunched { get; }
    }
    public interface IMergeLauncher
    {
        void MergeLaunch(Vector3 InheritedVelocity);
    }
    public class CubeMover : MonoBehaviour, ILaunching, IMergeLauncher
    {
        [SerializeField] private float _launchForce = 10f;
        [SerializeField] private float _mergeForce = 3f;

        private Rigidbody _rigidbody;

        private bool _isLaunched;

        public bool IsLaunched => _isLaunched;

        public void Init(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _isLaunched = false;
        }

        public void Launch()
        {
            _rigidbody.AddForce(Vector3.forward * _launchForce, ForceMode.Impulse);
            _isLaunched = true;
        }

        public void MergeLaunch(Vector3 inheritedVelocity)
        {
            _rigidbody.velocity = inheritedVelocity;
            _rigidbody.AddForce(Vector3.up * _mergeForce, ForceMode.Impulse);
        }
    }
}