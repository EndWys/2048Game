using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeMovingGameState : GameState
    {
        private IActiveCubeProvider _cubeProvider;
        private MainCubeEventBus _eventBus;

        public override void Enter()
        {
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _eventBus = ServiceLocator.Local.Get<MainCubeEventBus>();

            _eventBus.OnEvent += OnCubeEvent;
        }

        public override void Exit()
        {
            _eventBus.OnEvent -= OnCubeEvent;

            _cubeProvider.Clear();
        }

        private void OnCubeEvent(IMainCubeEvent evnt)
        {
            if (evnt is MainCubeMergedEvent || evnt is MainCubeSettled)
            {
                _stateSwitcher.SwitchState<CubeAimingGameState>();
            }
        }
    }
}