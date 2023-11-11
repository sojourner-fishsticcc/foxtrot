using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private float horizontalRaw;
    public Moving(PlayerMovementSM stateMachine) : base("Moving", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        horizontalRaw = 0f;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalRaw = Input.GetAxisRaw("Horizontal");
    } 
}
