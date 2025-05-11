namespace Assets._Project.Scripts.StateMachineSystem
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Tick();
    }
}