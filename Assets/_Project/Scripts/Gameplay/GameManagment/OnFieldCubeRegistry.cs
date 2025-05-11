using Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject;
using Assets._Project.Scripts.ServiceLocatorSystem;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Gameplay.GameManagment
{
    public interface IOnFieldCubeCounter
    {
        int CubeCount { get; }
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

        public int CubeCount => _onFieldCubes.Count;

        public void Register(Cube cube)
        {
            _onFieldCubes.Add(cube);
        }
        public void Unregister(Cube cube)
        {
            _onFieldCubes.Remove(cube);
        }

        public Cube[] GetRegistryArray() => _onFieldCubes.ToArray();
    }
}