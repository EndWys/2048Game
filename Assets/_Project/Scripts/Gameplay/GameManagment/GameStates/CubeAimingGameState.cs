using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeAimingGameState : GameState
    {
        private ICubeSpawner _cubeSpawner;
        private IActiveCubeProvider _cubeProvider;
        private ICubeAimController _aimController;

        public override void Enter()
        {
            _cubeSpawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _aimController = ServiceLocator.Local.Get<ICubeAimController>();

            var cube = _cubeSpawner.SpawnCube();
            _cubeProvider.SetCube(cube);

            _aimController.Enable(OnCubeLaunch);
        }

        public override void Exit()
        {
            _aimController.Disable();
        }

        private void OnCubeLaunch()
        {
            _cubeProvider.ActiveCube.Launch();
            _stateSwitcher.SwitchState<CubeMovingGameState>();
        }
    }
}