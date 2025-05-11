namespace Assets._Project.Scripts.Gameplay.CubeLogic.CubeObject.ÑubeComponents
{
    public interface IValueHolder
    {
        int Value { get; }

        void SetValue(int value);
    }
    public class CubeValue : IValueHolder
    {
        private CubeView _view;

        private int _value = 2;

        public int Value => _value;

        public CubeValue(CubeView cubeView)
        {
            _view = cubeView;

            _view.UpdateView(_value);
        }

        public void SetValue(int newValue)
        {
            _value = newValue;

            _view.UpdateView(_value);
        }

        public bool HasSameValue(CubeValue other)
        {
            return other != null && other.Value == _value;
        }
    }
}