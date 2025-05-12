using Assets._Project.Scripts.ServiceLocatorSystem;
using Assets._Project.Scripts.UI;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameEnterGameState : GameState
    {
        public override async void Enter()
        {
            await ServiceLocator.Local.Get<IReloadUI>().Hide();
            await ServiceLocator.Local.Get<IGameUI>().Show();
            _stateSwitcher.SwitchState<CubeAimingGameState>();
        }
    }
}