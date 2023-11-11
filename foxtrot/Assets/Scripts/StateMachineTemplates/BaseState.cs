using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;
    // creates new states derived from the basestate given a name /guess
    public BaseState(string name, StateMachine statemachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
