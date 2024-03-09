using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePatternInUnity
{
    public class StateMachine : MonoBehaviour
    {
        private IState _currentState;

        public void Initialize(IState startingState)
        {
            _currentState = startingState;
            startingState.Initialize(this);
        }

        public void ChangeState(IState newState)
        {
            _currentState.OnEnded();

            _currentState = newState;
            newState.Initialize(this);
        }

        private void Update()
        {
            _currentState?.Update();
        }
    }
}