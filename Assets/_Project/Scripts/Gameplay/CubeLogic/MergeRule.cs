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
        private MainCubeEventBus<MainCubeMergedEvent> _mergeEvent;

        public MergeRule()
        {
            _spawner = ServiceLocator.Local.Get<ICubeSpawner>();
            _mergeEvent = ServiceLocator.Local.Get<MainCubeEventBus<MainCubeMergedEvent>>();
        }

        public void TryMergeTwoCubes(Cube firstCube, Cube secondCube)
        {
            if(firstCube.CanMergeWith(secondCube))
            {
                int newValue = firstCube.ValueHolder.Value * 2;

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