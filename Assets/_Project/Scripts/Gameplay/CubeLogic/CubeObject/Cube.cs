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
    public class Cube : MonoBehaviour, ICubeComparer
    {
        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private CubeSettleHandler _cubeSettleHandler;
        [SerializeField] private CubeMergeHandler _cubeMergeHandler;
        [SerializeField] private Rigidbody _rigidBody;

        private IActiveCubeProvider _activeCubeProvider;

        public void Init()
        {
            _activeCubeProvider = ServiceLocator.Local.Get<IActiveCubeProvider>();

            _cubeMover.Init(_rigidBody);
            _cubeSettleHandler.Init(_rigidBody, _cubeMover);
            _cubeMergeHandler.Init(this, _rigidBody);
        }

        public bool IsMainCube()
        {
            return _activeCubeProvider.ActiveCube.Equals(this);
        }

        public void Launch() => _cubeMover.Launch();
        public void MergeLaunch(Vector3 velocity) => _cubeMover.MergeLaunch(velocity);
    }
}