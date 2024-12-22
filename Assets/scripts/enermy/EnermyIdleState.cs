using UnityEngine;

public class EnermyIdleState : EnermygGroundState
{
    public EnermyIdleState(Enermy _enermyBase, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _enermy) : base(_enermyBase, _stateMachine, _animBoolName,_enermy)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        enermy.zeroVelocity();
        stateTimer = enermy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0f)
        {
            stateMachine.ChangeState(enermy.MoveState);
        }
    }
}
