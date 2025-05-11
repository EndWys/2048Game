using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject
{
    public interface ICubeComparer
    {
        bool IsMainCube();
    }

    public interface IMergableCube
    {
        bool CanMergeWith(Cube cube);
    }
    public class Cube : MonoBehaviour, ICubeComparer
    {
        [SerializeField] private Rigidbody _rigidBody;

        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private CubeSettleHandler _cubeSettleHandler;
        [SerializeField] private CubeCollisionHandler _cubeCollisionHandler;
        [SerializeField] private CubeView _cubeView;

        private CubeValue _cubeValue;
        private CubeMergeHandler _cubeMergeHandler;

        private IActiveCubeProvider _activeCubeProvider;
        private IMergeRule _mergeRule;

        public IValueHolder ValueHolder => _cubeValue;

        public void Init()
        {
            _activeCubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();
            _mergeRule = ServiceLocator.Local.Get<IMergeRule>();

            _cubeValue = new CubeValue(_cubeView);
            _cubeMergeHandler = new CubeMergeHandler();

            _cubeMover.Init(_rigidBody);
            _cubeSettleHandler.Init(_rigidBody, _cubeMover);

            _cubeCollisionHandler.OnCubeCollide += OnCubeCollide;
        }

        public void Launch() => _cubeMover.Launch();
        public void MergeLaunch(Cube parent) => _cubeMover.MergeLaunch(parent._rigidBody.velocity);

        public void MakeMerged() => _cubeMergeHandler.MakeMerged();
        public bool IsMerged() => _cubeMergeHandler.IsMerged();
        public bool CanMergeWith(Cube cube) => _cubeMergeHandler.CanMergeWith(cube) && _cubeValue.HasSameValue(cube._cubeValue);

        public bool IsMainCube() => _activeCubeProvider.ActiveCube.Equals(this);

        private void OnCubeCollide(Cube otherCube)
        {
            _mergeRule.TryMergeTwoCubes(this, otherCube);
        }

        private void OnDestroy()
        {
            _cubeCollisionHandler.OnCubeCollide -= OnCubeCollide;
        }
    }
}