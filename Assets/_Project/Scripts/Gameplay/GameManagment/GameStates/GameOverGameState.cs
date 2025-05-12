using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Assets._Project.Scripts.UI;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameOverGameState : GameState
    {
        private IGameScore _gameScore;
        private ICubeSpawner _cubeSpawner;

        private IGameOverdUI _gameOverdUI;
        private IReloadUI _reloadUI;
        private IGameUI _gameUI;

        public override async void Enter()
        {
            _gameScore = ServiceLocator.Local.Get<IGameScore>();
            _cubeSpawner = ServiceLocator.Local.Get<ICubeSpawner>();

            _gameOverdUI = ServiceLocator.Local.Get<IGameOverdUI>();
            _reloadUI = ServiceLocator.Local.Get<IReloadUI>();
            _gameUI = ServiceLocator.Local.Get<IGameUI>();

            await _gameUI.Hide();
            await _gameOverdUI.Show();

            _gameOverdUI.OnTouch += OnTouch;
        }

        private async void OnTouch()
        {
            _gameOverdUI.OnTouch -= OnTouch;

            await _reloadUI.Show();
            await _gameOverdUI.Hide();
            _cubeSpawner.DespawnAllCubes();
            _gameScore.ResetScore();
            await _reloadUI.Hide();
            await _gameUI.Show();

            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}