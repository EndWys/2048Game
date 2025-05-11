using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

public class CubeSettleHandler : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private ILaunching _launching;

    private MainCubeEventBus<MainCubeSettledEvent> _settaledEvent;

    private bool _isSettled = false;

    private float _settleThreshold = 0.1f;
    private float _settleTime = 0.2f;
    private float _timer = 0f;

    public void Init(Rigidbody rigidbody, ILaunching launching)
    {
        _isSettled = false;
        _timer = 0f;

        _rigidBody = rigidbody;
        _launching = launching;

        _settaledEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeSettledEvent>>();
    }

    private void Update()
    {
        if (!_launching.IsLaunched) return;

        if (_isSettled) return;

        if (_rigidBody.velocity.magnitude < _settleThreshold)
        {
            _timer += Time.deltaTime;
            if (_timer >= _settleTime)
            {
                _isSettled = true;
                _settaledEvent.Raise(new MainCubeSettledEvent());
            }
        }
        else
        {
            _timer = 0f;
        }
    }
}
