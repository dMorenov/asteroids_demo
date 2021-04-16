using UnityEngine;

namespace GameCore
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State newState)
        {
            State = newState;
            StartCoroutine(State.Start());
        }
    }
}