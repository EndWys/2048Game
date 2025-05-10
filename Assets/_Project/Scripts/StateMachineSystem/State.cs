namespace Assets._Project.Scripts.StateMachineSystem
{
    public abstract class State<TState> : IState where TState : IState
    {
        protected IStateSwitcher<TState> _stateSwitcher;

        public void SetSwitcher(IStateSwitcher<TState> stateSwitcher)
        {
            if (_stateSwitcher == null)
                _stateSwitcher = stateSwitcher;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Tick();
    }
}