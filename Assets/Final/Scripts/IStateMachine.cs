using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    IStateBehaviour CurrentState { get; set; }

    object Context { get; set; }

    void SolveBehavior();
    
    public void ChangeState(IStateBehaviour nextState)
    {
        CurrentState = nextState;
        CurrentState.OnEnter(Context);
    }
}
