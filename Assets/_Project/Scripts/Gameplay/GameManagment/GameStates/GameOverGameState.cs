using Assets._Project.Scripts.Gameplay.GameplayInput;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameOverGameState : GameState
    {
        private IInputHandler _input;

        private IGameScore _gameScore;

        public override void Enter()
        {
            _input = ServiceLocator.Local.Get<IInputHandler>();
            _gameScore = ServiceLocator.Local.Get<IGameScore>();

            _input.OnTouchUp += OnTouch;
        }

        public override void Exit()
        {
            _gameScore.ResetScore();
        }

        private void OnTouch(Vector2 pos)
        {
            _input.OnTouchUp -= OnTouch;
            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}