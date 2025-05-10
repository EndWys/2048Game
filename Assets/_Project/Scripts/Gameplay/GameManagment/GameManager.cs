using Assets._Project.Scripts.Gameplay.GameManagment.GameStates;
using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public class GameManager : IService
    {
        private GameStateMachine _gameStateMachine;

        public GameManager()
        {
            RegisterGameStates();
        }

        private void RegisterGameStates()
        {
            _gameStateMachine = new GameStateMachine();

            _gameStateMachine.Register(new CubeAimingGameState());
            _gameStateMachine.Register(new CubeMovingGameState());
            _gameStateMachine.Register(new GameOverGameState());
        }

        public void StartGame() => _gameStateMachine.SwitchState<CubeAimingGameState>();

        public void Tick()
        {
            _gameStateMachine.Tick();
        }
    }
}