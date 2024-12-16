using UnityEngine;

public class Enermy : Entity
{
    #region
    public EnermyStateMachine StateMachine {  get; private set; }
    #endregion
    #region
    public EnermyStateMachine stateMachine { get; private set; }
    public EnermyIdleState idleState { get; private set; }
    public EnermyAttackState attackState { get; private set; }
    public EnermyHitState hitState { get; private set; }
    public EnermyMoveState moveState { get; private set; }
    public EnermyDeadState deadState { get; private set; }
    

    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnermyStateMachine();
        idleState = new EnermyIdleState(this, stateMachine, "Idle");
        attackState = new EnermyAttackState(this, stateMachine, "Attack");
        hitState = new EnermyHitState(this, stateMachine, "Hit");
        moveState = new EnermyMoveState(this, stateMachine, "Move");
        deadState = new EnermyDeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
