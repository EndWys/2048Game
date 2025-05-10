using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _launchForce = 10f;

    private bool _isSettled = false;
    private bool _isLaunched = false;

    private float _settleThreshold = 0.1f;
    private float _settleTime = 0.2f;
    private float _timer = 0f;

    private MainCubeEventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Local.Get<MainCubeEventBus>();
    }

    public void Launch()
    {
        _rigidBody.AddForce(Vector3.forward * _launchForce, ForceMode.Impulse);
        _isLaunched = true;
    }

    private void Update()
    {
        if (!_isLaunched) return;

        if (_isSettled) return;

        if (_rigidBody.velocity.magnitude < _settleThreshold)
        {
            _timer += Time.deltaTime;
            if (_timer >= _settleTime)
            {
                Debug.Log("IsSattled");
                _isSettled = true;
                _eventBus.Raise(new MainCubeSettled());
            }
        }
        else
        {
            _timer = 0f;
        }
    }

    private void MergeWith(Cube otherCube)
    {
        // merge logic

        _eventBus.Raise(new MainCubeMergedEvent());
    }
}
