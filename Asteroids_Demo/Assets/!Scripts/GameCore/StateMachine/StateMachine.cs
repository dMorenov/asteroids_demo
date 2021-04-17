using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;
        private Dictionary<Type, State> _typeToState;

        protected void InitializeStateMachine(Dictionary<Type, State> states)
        {
            _typeToState = states;
        }

        public void SetState(Type newState)
        {
            State = _typeToState[newState];
            StartCoroutine(State.Start());
        }
    }
}