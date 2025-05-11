using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents;
using Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll;
using Assets._Project.Scripts.ObjectPoolSytem;
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
    public class Cube : PoolObject, ICubeComparer
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
        }

        public void SetPosition(Vector3 position) => _rigidBody.position = position;
        public void SetRotation(Quaternion rotation) => _rigidBody.rotation = rotation;

        public void Launch() => _cubeMover.Launch();
        public void MergeLaunch(Cube parent) => _cubeMover.MergeLaunch(parent._rigidBody.velocity);

        public void Deactivate() => CachedGameObject.SetActive(false);

        public void MakeMerged() =>_cubeMergeHandler.MakeMerged();
        public bool IsMerged() => _cubeMergeHandler.IsMerged();
        public bool CanMergeWith(Cube cube) => _cubeMergeHandler.CanMergeWith(cube) && _cubeValue.HasSameValue(cube._cubeValue);

        public bool IsMainCube() => _activeCubeProvider.ActiveCube.Equals(this);

        public override void OnGetFromPool()
        {
            ReInit();

            CachedGameObject.SetActive(true);
            _rigidBody.isKinematic = false;

            _cubeCollisionHandler.OnCubeCollide += OnCubeCollide;
        }

        public override void OnReleaseToPool()
        {
            _cubeCollisionHandler.OnCubeCollide -= OnCubeCollide;

            _rigidBody.velocity = Vector3.zero;
            _rigidBody.isKinematic = true;

            CachedGameObject.SetActive(false);
        }

        private void ReInit()
        {
            _cubeValue = new CubeValue(_cubeView);
            _cubeMergeHandler = new CubeMergeHandler();

            _cubeMover.Init(_rigidBody);
            _cubeSettleHandler.Init(_rigidBody, _cubeMover);
        }

        private void OnCubeCollide(Cube otherCube)
        {
            _mergeRule.TryMergeTwoCubes(this, otherCube);
        }
    }
}