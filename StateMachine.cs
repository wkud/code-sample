using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Common.Patterns
{
    public abstract class StateMachine<T> : MonoBehaviour where T : IState
    {
        protected abstract T InitialState { get; }
        protected abstract List<T> States { get; }

        protected T currentState;

        protected virtual void Start()
        {
            currentState = InitialState;
        }

        protected void SwitchState(T state)
        {
            if (state == null)
            {
                Debug.LogError("StateMachine tried to switch to undefined state");
                return;
            }

            currentState.DisableState();
            currentState = state;
            currentState.EnableState();
        }

        protected void SwitchState(Func<T, bool> predicate) => SwitchState(FindState(predicate));

        protected void SwitchToInitialState() => SwitchState(InitialState);

        protected T FindState(Func<T, bool> predicate) => States.FirstOrDefault(predicate);

    }
}