using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private float horizontalRaw;
    public Idle(PlayerMovementSM stateMachine) : base("Idle", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        horizontalRaw = 0f;
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalRaw = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(horizontalRaw) > Mathf.Epsilon)
    }
}
