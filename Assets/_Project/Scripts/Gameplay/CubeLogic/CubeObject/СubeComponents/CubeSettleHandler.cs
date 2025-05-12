using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;

public class CubeSettleHandler : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private ILaunching _launching;

    private bool _isSettled = false;

    private float _settleThreshold = 0.1f;
    private float _settleTime = 0.2f;
    private float _timer = 0f;

    public event Action OnCubeSettle;

    public void Init(Rigidbody rigidbody, ILaunching launching)
    {
        _isSettled = false;
        _timer = 0f;

        _rigidBody = rigidbody;
        _launching = launching;
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
                OnCubeSettle?.Invoke();
            }
        }
        else
        {
            _timer = 0f;
        }
    }
}
