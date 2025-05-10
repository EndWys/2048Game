using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.StateMachineSystem
{
    public abstract class GenericStateMachine<TState> : IStateSwitcher<TState> where TState : State<TState>
    {
        private readonly Dictionary<Type, TState> _states = new();
        private TState _currentState;

        public void Register<T>(T state) where T : TState
        {
            state.SetSwitcher(this);
            _states[typeof(T)] = state;
        }

        public void SwitchState<T>() where T : TState
        {
            _currentState?.Exit();
            if (_states.TryGetValue(typeof(T), out var newState))
            {
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(T).Name} not registered.");
            }
        }

        public void Tick()
        {
            _currentState?.Tick();
        }
    }
}