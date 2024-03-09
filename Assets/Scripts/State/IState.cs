using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePatternInUnity
{
    public abstract class IState
    {
        public void Initialize(StateMachine stateMachine)
        {
            OnInitialized();
            OnActive();
        }

        public virtual void OnInitialized()
        {

        }

        public virtual void OnActive()
        {

        }

        public virtual void Update()
        {

        }

        public virtual IEnumerator DoLogic()
        {
            yield return null;
        }

        public virtual void OnEnded()
        {

        }
    }
}