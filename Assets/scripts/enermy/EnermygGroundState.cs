using UnityEngine;

public class EnermygGroundState : EnermyState
{
    protected Enermy_Skeleton enermy;
    public EnermygGroundState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
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
        if (enermy.isPlayerDetected())
        {
            stateMachine.ChangeState(enermy.BattleState);
        }
    }
}
