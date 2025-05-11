using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets._Project.Scripts.Gameplay.CubeLogic
{
    public interface IMergeRule : IService
    {
        public void TryMergeTwoCubes(Cube firstCube, Cube secondCube);
    }
    public class MergeRule : IMergeRule
    {
        private ICubeSpawner _spawner;
        private IGameScore _gameScore;

        private MainCubeEventBus<MainCubeMergedEvent> _mergeEvent;

        public MergeRule()
        {
            _spawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _gameScore = ServiceLocator.Local.Get<IGameScore>();

            _mergeEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeMergedEvent>>();
        }

        public void TryMergeTwoCubes(Cube firstCube, Cube secondCube)
        {
            if(firstCube.CanMergeWith(secondCube))
            {
                int parentCubeValue = firstCube.ValueHolder.Value;

                _gameScore.AddScore(parentCubeValue / 2);

                int newValue = parentCubeValue * 2;

                firstCube.MakeMerged();
                secondCube.MakeMerged();

                Vector3 spawnPos = (firstCube.transform.position + secondCube.transform.position) / 2f;

                _spawner.DespawnCube(firstCube);
                _spawner.DespawnCube(secondCube);

                Cube newCube = _spawner.SpawnCubeOnPosition(spawnPos);

                newCube.Init();
                newCube.MergeLaunch(firstCube);
                newCube.ValueHolder.SetValue(newValue);

                if (firstCube.IsMainCube() || secondCube.IsMainCube())
                {
                    _mergeEvent.Raise(new());
                }
            }
        }
    }
}