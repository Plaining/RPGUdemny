using UnityEngine;

public class EnermygGroundState : EnermyState
{
    protected Enermy_Skeleton enermy;
    protected Transform player;
    public EnermygGroundState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enermy.isPlayerDetected() || Vector2.Distance(enermy.transform.position, player.position) < 2)
        {
            stateMachine.ChangeState(enermy.BattleState);
        }
    }
}
