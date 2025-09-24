using UnityEngine;

public class SkeletonStunnedState : EnermyState
{
    private Enermy_Skeleton enermy;
    public SkeletonStunnedState(Enemy _enermyBase, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _enermy) : base(_enermyBase, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        enermy.fx.InvokeRepeating("RedColerBlink", 0, .1f);
        stateTimer = enermy.stunnedDuration;
        rb.linearVelocity = new Vector2(-enermy.facingDir * enermy.stunnedDirection.x, enermy.stunnedDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enermy.fx.Invoke("CancelRedBlink",0);
    }

    public override void Update()
    {
        base.Update();
        /*if (enemy.isGroundDetected())
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }*/
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enermy.IdleState);
        }
    }
}
