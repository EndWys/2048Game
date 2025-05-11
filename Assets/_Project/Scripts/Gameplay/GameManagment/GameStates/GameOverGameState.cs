using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class GameOverGameState : GameState
    {
        private IGameScore _gameScore;

        public override void Enter()
        {
            _gameScore = ServiceLocator.Local.Get<IGameScore>();
        }

        public override void Exit()
        {
            _gameScore.ResetScore();
        }
    }
}