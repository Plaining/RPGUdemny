using UnityEngine;

public class SkeletonStunnedState : EnermyState
{
    private Enermy_Skeleton enermy;
    public SkeletonStunnedState(Enermy _enermyBase, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _enermy) : base(_enermyBase, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enermy.stunnedDuration;
        rb.linearVelocity = new Vector2(-enermy.facingDir * enermy.stunnedDirection.x, enermy.stunnedDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        /*if (enermy.isGroundDetected())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }*/
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enermy.IdleState);
        }
    }
}
