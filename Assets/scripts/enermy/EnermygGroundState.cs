using UnityEngine;

public class EnermygGroundState : EnermyState
{
    protected Enermy_Skeleton enermy;
    protected Transform player;
    public EnermygGroundState(Enemy _enermyBase, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _enermy) : base(_enermyBase, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;

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
