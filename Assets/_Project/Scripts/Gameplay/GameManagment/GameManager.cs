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

            _gameStateMachine.Register(new GameEnterGameState());
            _gameStateMachine.Register(new CubeAimingGameState());
            _gameStateMachine.Register(new CubeMovingGameState());
            _gameStateMachine.Register(new GameOverGameState());
            _gameStateMachine.Register(new GameExitGameState());
        }

        public void StartGame() => _gameStateMachine.SwitchState<GameEnterGameState>();
        public void ExitGame() => _gameStateMachine.SwitchState<GameExitGameState>();

        public void Tick()
        {
            _gameStateMachine.Tick();
        }
    }
}