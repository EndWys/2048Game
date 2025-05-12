using Assets._Project.Scripts.Effects;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.GameManagment.GameStates
{
    public class CubeAimingGameState : GameState
    {
        private ICubeSpawner _cubeSpawner;
        private IActiveCubeProvider _cubeProvider;
        private ICubeAimController _aimController;

        private SoundManager _soundManager;

        private float _cubeSpawnDelay = 0.25f;

        private CancellationTokenSource _cancellationTokenSource;

        public override async void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _soundManager = ServiceLocator.Global.Get<SoundManager>();

            _cubeSpawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _cubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _aimController = ServiceLocator.Local.Get<ICubeAimController>();

            await UniTask.Delay(TimeSpan.FromSeconds(_cubeSpawnDelay), cancellationToken: _cancellationTokenSource.Token);

            Cube cube = _cubeSpawner.SpawnMainCube();

            _cubeProvider.SetCube(cube);

            _aimController.Enable(OnCubeLaunch);

            _soundManager.PlayCubeSpawn();
        }

        public override void Exit()
        {
            _aimController.Disable();

            _cancellationTokenSource?.Cancel();
        }

        private void OnCubeLaunch()
        {
            _soundManager.PlayCubeLounch();
            _cubeProvider.ActiveCube.Launch();
            _stateSwitcher.SwitchState<CubeMovingGameState>();
        }
    }
}