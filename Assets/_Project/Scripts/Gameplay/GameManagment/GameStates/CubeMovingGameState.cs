using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeMovingGameState : GameState
    {
        private IActiveCubeProvider _cubeProvider;

        private MainCubeEventBus<MainCubeSettledEvent> _settaledEvent;
        private MainCubeEventBus<MainCubeMergedEvent> _mergeEvent;

        public override void Enter()
        {
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();

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


        private void OnSettaledEvent(MainCubeSettledEvent evnt)
        {
            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }

        private void OnMergedEvent(MainCubeMergedEvent evnt)
        {
            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}