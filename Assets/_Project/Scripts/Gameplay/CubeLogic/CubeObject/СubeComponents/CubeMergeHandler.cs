namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public class CubeMergeHandler
    {
        private bool _merged = false;

        public void MakeMerged()
        {
            _merged = true;
        }

        public bool IsMerged()
        {
            return _merged;
        }

        public bool CanMergeWith(Cube otherCube)
        {
            if (_merged)
                return false;

            if (otherCube.IsMerged())
                return false;

            return true;
        }
    }
}