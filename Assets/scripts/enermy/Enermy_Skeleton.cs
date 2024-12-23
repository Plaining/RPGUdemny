using UnityEngine;

public class Enermy_Skeleton : Enermy
{
    #region
    public EnermyIdleState IdleState { get; private set; }
    public EnermyAttackState AttackState { get; private set; }
    public EnermyBattleState BattleState { get; private set; }
    public EnermyMoveState MoveState { get; private set; }
    public EnermyDeadState DeadState { get; private set; }
    public SkeletonStunnedState StunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        IdleState = new EnermyIdleState(this, StateMachine, "Idle", this);
        AttackState = new EnermyAttackState(this, StateMachine, "Attack", this);
        BattleState = new EnermyBattleState(this, StateMachine, "Move", this);
        MoveState = new EnermyMoveState(this, StateMachine, "Move", this);
        DeadState = new EnermyDeadState(this, StateMachine, "Dead");
        StunnedState = new SkeletonStunnedState(this, StateMachine, "Stunned", this);
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(MoveState);
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
        {
            StateMachine.ChangeState(StunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(StunnedState);
            return true;
        }
        else
        {
            return false;
        }
    }
}
