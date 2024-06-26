﻿using System;
using System.Collections.Generic;

namespace Flags.Services
{
    public class GameStateMachine
    {
        private IState ActiveState { get; set; }
        private readonly Dictionary<Type, IState> _states = new();

        public void AddState<TState>(TState state) where TState : class, IState =>
            _states.Add(typeof(TState), state);

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state?.Enter();
        }
        
        private TState ChangeState<TState>() where TState : class, IState
        {
            ActiveState?.Exit();

            TState state = GetState<TState>();
            ActiveState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}