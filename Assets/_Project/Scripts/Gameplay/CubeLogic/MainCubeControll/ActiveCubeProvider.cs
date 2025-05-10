using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets._Project.Scripts.Gameplay.CubeLogic.MainCubeControll
{
    public interface IActiveCubeProvider : IService
    {
        Cube ActiveCube { get; }
        void SetCube(Cube cube);
        void Clear();
    }

    public class ActiveCubeProvider : IActiveCubeProvider
    {
        public Cube ActiveCube { get; private set; }

        public void SetCube(Cube cube)
        {
            ActiveCube = cube;
        }

        public void Clear()
        {
            ActiveCube = null;
        }
    }
}