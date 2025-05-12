using Assets._Project.Scripts.Effects;
using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

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

        private int _devideScoreBy = 2;
        private int _newValueMultiplier = 2;

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

                _gameScore.AddScore(parentCubeValue / _devideScoreBy);

                int newValue = parentCubeValue * _newValueMultiplier;

                firstCube.MakeMerged();
                secondCube.MakeMerged();

                firstCube.Deactivate();
                secondCube.Deactivate();

                Vector3 spawnPos = (firstCube.transform.position + secondCube.transform.position) / 2f;

                Cube newCube = _spawner.SpawnCubeOnPosition(spawnPos);

                newCube.MergeLaunch(firstCube);
                newCube.ValueHolder.SetValue(newValue);

                _spawner.DespawnCube(firstCube);
                _spawner.DespawnCube(secondCube);

                if (firstCube.IsMainCube() || secondCube.IsMainCube())
                {
                    _mergeEvent.Raise(new());
                }

                ServiceLocator.Global.Get<SoundManager>().PlayCubeMerge();
            }
        }
    }
}