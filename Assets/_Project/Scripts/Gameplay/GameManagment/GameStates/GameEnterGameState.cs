using Assets._Project.Scripts.ServiceLocatorSystem;
using Assets._Project.Scripts.UI;
using System.Threading;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameEnterGameState : GameState
    {
        private CancellationTokenSource _cancellationTokenSource;

        public override async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await ServiceLocator.Local.Get<IReloadUI>().Hide().SuppressCancellationThrow();
            await ServiceLocator.Local.Get<IGameUI>().Show().SuppressCancellationThrow();

            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }

        public override void Exit()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}