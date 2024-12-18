using UnityEngine;

public class EnermyIdleState : EnermygGroundState
{
    public EnermyIdleState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
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
