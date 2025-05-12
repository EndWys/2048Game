using Assets._Project.Scripts.Gameplay.GameplayInput;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface ICubeAimController : IService
    {
        void Enable(Action onLaunch);
        void Disable();
    }

    public class CubeAimController : ICubeAimController
    {
        private readonly IInputHandler _input;
        private readonly IActiveCubeProvider _cubeProvider;

        private Action _onLaunch;
        private Vector2 _startTouchPosition;
        private float _moveLimit = 1.95f;
        private float _sensitivity = 0.01f;

        public CubeAimController()
        {
            _input = ServiceLocator.Local.Get<IInputHandler>();
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
        }

        public void Enable(Action onLaunch)
        {
            _onLaunch = onLaunch;
            _input.OnTouchDown += HandleTouchDown;
            _input.OnTouchMove += HandleTouchMove;
            _input.OnTouchUp += HandleTouchUp;
        }

        public void Disable()
        {
            _input.OnTouchDown -= HandleTouchDown;
            _input.OnTouchMove -= HandleTouchMove;
            _input.OnTouchUp -= HandleTouchUp;
            _onLaunch = null;
        }

        private void HandleTouchDown(Vector2 pos)
        {
            _startTouchPosition = pos;
        }

        private void HandleTouchMove(Vector2 currentPosition)
        {
            var cube = _cubeProvider.ActiveCube;
            if (cube == null) return;

            float deltaX = (currentPosition.x - _startTouchPosition.x) * _sensitivity;
            Vector3 newPosition = cube.CachedTrasform.position;
            newPosition.x = Mathf.Clamp(newPosition.x + deltaX, -_moveLimit, _moveLimit);
            cube.MoveCubeBody(newPosition);

            _startTouchPosition = currentPosition;
        }

        private void HandleTouchUp(Vector2 pos)
        {
            _onLaunch?.Invoke();
        }
    }
}