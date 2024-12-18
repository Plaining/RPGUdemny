using UnityEngine;

public class Enermy_Skeleton : Enermy
{
    #region
    public EnermyIdleState IdleState { get; private set; }
    public EnermyAttackState AttackState { get; private set; }
    public EnermyBattleState BattleState { get; private set; }
    public EnermyHitState HitState { get; private set; }
    public EnermyMoveState MoveState { get; private set; }
    public EnermyDeadState DeadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        IdleState = new EnermyIdleState(this, StateMachine, "Idle");
        AttackState = new EnermyAttackState(this, StateMachine, "Attack");
        BattleState = new EnermyBattleState(this, StateMachine, "Move");
        HitState = new EnermyHitState(this, StateMachine, "Hit");
        MoveState = new EnermyMoveState(this, StateMachine, "Move");
        DeadState = new EnermyDeadState(this, StateMachine, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(MoveState);
    }

}
