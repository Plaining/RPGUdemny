using UnityEngine;

public class EnermyAttackState : EnermyState
{
    private Enermy_Skeleton enermy;

    public EnermyAttackState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
    {
        enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        //enermy.zeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enermy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enermy.zeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enermy.BattleState);
        }
    }
}
