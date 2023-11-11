using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSM : StateMachine
{
    public Idle idleState;
    public Moving movingState;

    private void Awake() // something something sets up the idle and running states as "Idle" and "Running" /guess
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
