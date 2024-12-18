using UnityEngine;

public class EnermyMoveState : EnermygGroundState
{
    public EnermyMoveState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
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
            stateMachine.ChangeState(enermy.IdleState);
        }
    }
}
