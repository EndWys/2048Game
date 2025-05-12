using Assets._Project.Scripts.Effects;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Assets._Project.Scripts.UI;
using System.Threading;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameOverGameState : GameState
    {
        private IGameScore _gameScore;
        private ICubeSpawner _cubeSpawner;

        private IGameOverdUI _gameOverdUI;
        private IReloadUI _reloadUI;
        private IGameUI _gameUI;

        private CancellationTokenSource _cancellationTokenSource;

        public override async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _gameScore = ServiceLocator.Local.Get<IGameScore>();
            _cubeSpawner = ServiceLocator.Local.Get<ICubeSpawner>();

            _gameOverdUI = ServiceLocator.Local.Get<IGameOverdUI>();
            _reloadUI = ServiceLocator.Local.Get<IReloadUI>();
            _gameUI = ServiceLocator.Local.Get<IGameUI>();

            //_soundManager.PlayGameOver();

            await _gameUI.Hide().SuppressCancellationThrow();
            await _gameOverdUI.Show().SuppressCancellationThrow();

            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            _gameOverdUI.OnTouch += OnTouch;
        }

        public override void Exit()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private async void OnTouch()
        {
            _gameOverdUI.OnTouch -= OnTouch;

            await _reloadUI.Show().SuppressCancellationThrow();
            await _gameOverdUI.Hide().SuppressCancellationThrow();

            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            _cubeSpawner.DespawnAllCubes();
            _gameScore.ResetScore();

            await _reloadUI.Hide().SuppressCancellationThrow();
            await _gameUI.Show().SuppressCancellationThrow();

            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}