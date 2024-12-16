using UnityEngine;

public class EnermyMoveState : EnermyState
{
    public EnermyMoveState(Enermy _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enermy.SetVelocity(enermy.facingDir * enermy.moveSpeed, rb.linearVelocityY);

        if (!enermy.isGroundDetected() || enermy.isWallDetected())
        {
            enermy.Flip();
        }
    }
}
