using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public interface IOnFieldCubeCounter
    {
        int CubeCount { get; }

        event Action<int> OnCountChange;
    }

    public interface IOnFieldCubeRegister
    {
        void Register(Cube cube);
        void Unregister(Cube cube);

        Cube[] GetRegistryArray();
    }

    public class OnFieldCubeRegistry : IService, IOnFieldCubeRegister, IOnFieldCubeCounter
    {
        private List<Cube> _onFieldCubes = new List<Cube>();

        public event Action<int> OnCountChange;

        public int CubeCount => _onFieldCubes.Count;

        public void Register(Cube cube)
        {
            _onFieldCubes.Add(cube);
            OnCountChange?.Invoke(CubeCount);
        }
        public void Unregister(Cube cube)
        {
            _onFieldCubes.Remove(cube);
            OnCountChange?.Invoke(CubeCount);
        }

        public Cube[] GetRegistryArray() => _onFieldCubes.ToArray();
    }
}