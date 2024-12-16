using UnityEngine;

public class EnermyIdleState : EnermyState
{
    public EnermyIdleState(Enermy _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
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
    }
}
