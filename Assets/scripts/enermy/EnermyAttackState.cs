using UnityEngine;

public class EnermyAttackState : EnermyState
{
    private Enermy_Skeleton enermy;

    public EnermyAttackState(Enermy _enermyBase, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _enermy) : base(_enermyBase, _stateMachine, _animBoolName)
    {
        enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        //enermy.setZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        enermy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enermy.setZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enermy.BattleState);
        }
    }
}
