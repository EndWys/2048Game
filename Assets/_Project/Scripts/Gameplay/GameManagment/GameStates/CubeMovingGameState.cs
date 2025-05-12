using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeMovingGameState : GameState
    {
        private IActiveCubeProvider _cubeProvider;
        private IGameOverChecker _gameOverChecker;

        private MainCubeEventBus<MainCubeSettledEvent> _settaledEvent;
        private MainCubeEventBus<MainCubeMergedEvent> _mergeEvent;

        public override void Enter()
        {
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _gameOverChecker = ServiceLocator.Local.Get<IGameOverChecker>();

            _settaledEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeSettledEvent>>();
            _mergeEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeMergedEvent>>();

            _settaledEvent.OnEvent += OnSettaledEvent;
            _mergeEvent.OnEvent += OnMergedEvent;
        }

        public override void Exit()
        {
            _settaledEvent.OnEvent -= OnSettaledEvent;
            _mergeEvent.OnEvent -= OnMergedEvent;

            _cubeProvider.Clear();
        }


        private void OnSettaledEvent(MainCubeSettledEvent evnt) => OnAnyEvent();

        private void OnMergedEvent(MainCubeMergedEvent evnt) => OnAnyEvent();

        private void OnAnyEvent()
        {
            if (_gameOverChecker.IsGameOver())
            {
                _stateSwitcher.SwitchState<GameOverGameState>();
                return;
            }

            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}