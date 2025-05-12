using Assets._Project.Scripts.Effects;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeAimingGameState : GameState
    {
        private ICubeSpawner _cubeSpawner;
        private IActiveCubeProvider _cubeProvider;
        private ICubeAimController _aimController;

        private IGameOverChecker _gameOverChecker;

        private SoundManager _soundManager;

        public override void Enter()
        {
            _soundManager = ServiceLocator.Global.Get<SoundManager>();

            _gameOverChecker = ServiceLocator.Local.Get<IGameOverChecker>();

            if (_gameOverChecker.IsGameOver())
            {
                _soundManager.PlayGameOver();
                _stateSwitcher.SwitchState<GameOverGameState>();
                return;
            }

            _cubeSpawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _aimController = ServiceLocator.Local.Get<ICubeAimController>();

            Cube cube = _cubeSpawner.SpawnMainCube();

            _cubeProvider.SetCube(cube);

            _aimController.Enable(OnCubeLaunch);

            _soundManager.PlayCubeSpawn();
        }

        public override void Exit()
        {
            _aimController.Disable();
        }

        private void OnCubeLaunch()
        {
            _soundManager.PlayCubeLounch();
            _cubeProvider.ActiveCube.Launch();
            _stateSwitcher.SwitchState<CubeMovingGameState>();
        }
    }
}