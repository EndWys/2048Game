using Assets._Project.Scripts.Gameplay.CubeLogic;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.Gameplay.GameManagment;
using Assets._Project.Scripts.Gameplay.GameplayInput;
using Assets._Project.Scripts.UI;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Game : MonoBehaviour
    {
        [SerializeField] private GameplaySettings _gameplaySettings;
        [SerializeField] private CubeSpawner _cubeSpawner;

        [Header("UI")]
        [SerializeField] private ReloadUIPanel _reloadUIPanel;
        [SerializeField] private GameUIPanel _gameUIPanel;
        [SerializeField] private GameOverUIPanel _gameOverUIPanel;

        private ServiceLocator _local;

        public void Load()
        {
            _local = ServiceLocator.CreateLocalSceneServiceLocator();

            RegisterAllServices();
        }

        private void RegisterAllServices()
        {
            _local.Register(_gameplaySettings);

            _local.Register<IInputHandler>(new InputHandler());

            _local.Register(new MainCubeEventBus<MainCubeSettledEvent>());
            _local.Register(new MainCubeEventBus<MainCubeMergedEvent>());

            _local.Register<IGameScore>(new GameScore());
            _local.Register(new OnFieldCubeRegistry());

            _local.Register<IActiveCubeProvider>(new ActiveCubeProvider());
            _cubeSpawner.Init();
            _local.Register<ICubeSpawner>(_cubeSpawner);
            _local.Register<ICubeAimController>(new CubeAimController());

            _local.Register<IMergeRule>(new MergeRule());

            _local.Register<IGameOverChecker>(new GameOverChecker());

            _reloadUIPanel.Init();
            _local.Register<IReloadUI>(_reloadUIPanel);
            _gameUIPanel.Init();
            _local.Register<IGameUI>(_gameUIPanel);
            _gameOverUIPanel.Init();
            _local.Register<IGameOverdUI>(_gameOverUIPanel);


            _local.Register(new GameManager());
        }

        private void OnDestroy()
        {
            UnregisterAllServices();
        }

        private void UnregisterAllServices()
        {
            if (_local == null)
                return;

            _local.Unregister<IInputHandler>();
            _local.Unregister<GameManager>();
        }
    }
}