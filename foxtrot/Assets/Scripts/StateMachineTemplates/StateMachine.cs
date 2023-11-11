using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    void Start() // grabs the current state on startup. if it exists, it runs its enter parameter
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update() // first runs the state's logic
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }
    void LateUpdate() // second runs the state's physics
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }
    public void ChangeState(BaseState newState) // transitions between different states
    {
        print("exiting current state");
        currentState.Exit();
        print("exited current state");
        // exits the current state, then redefines the current state to the one being changed to and runs it's enter term
        currentState = newState;
        print("redefined state");
        currentState.Enter();
        print("entered new state");
    }
    protected virtual BaseState GetInitialState() // initializes the current state. this does nothing because it's a template, the initialState is given by the actual state machine that uses this template
    {
        return null;
    }
    private void OnGUI() // adds a gui displaying the current state
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUI.Label (new Rect(0,0,80,20), content);
    }
}
