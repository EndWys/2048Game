using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public interface IGameOverChecker : IService
    {
        bool IsGameOver();
    }

    public class GameOverChecker : IGameOverChecker
    {
        private IOnFieldCubeCounter _cubeCounter;
        private IActiveCubeProvider _cubeProvider;

        private GameplaySettings _settings;

        public GameOverChecker()
        {
            _cubeCounter = ServiceLocator.Local.Get<OnFieldCubeRegistry>();
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();

            _settings = ServiceLocator.Local.Get<GameplaySettings>();
        }

        public bool IsGameOver()
        {
            //MainCube still not exit spawn point
            if (_cubeProvider.ActiveCube?.CachedGameObject.layer == LayerMask.NameToLayer(Cube.MAIN_CUBE_LAYER))
                return true;

            bool result = _cubeCounter.CubeCount >= _settings.LoseCubesCountOnField;

            return result;
        }
    }
}