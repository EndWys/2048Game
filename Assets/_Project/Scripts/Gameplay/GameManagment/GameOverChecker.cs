using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public interface IGameOverChecker : IService
    {
        bool IsGameOver();
    }

    public class GameOverChecker : IGameOverChecker
    {
        private IOnFieldCubeCounter _cubeCounter;

        private GameplaySettings _settings;

        public GameOverChecker()
        {
            _cubeCounter = ServiceLocator.Local.Get<OnFieldCubeRegistry>();
            _settings = ServiceLocator.Local.Get<GameplaySettings>();
        }

        public bool IsGameOver()
        {
            bool result = _cubeCounter.CubeCount >= _settings.LoseCubesCountOnField;

            return result;
        }
    }
}