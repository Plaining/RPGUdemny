using UnityEngine;

public class Enermy : Entity
{
    [Header("PlayerCheck info")]
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] protected LayerMask WhatIsPlayer;
    [Header("Attack info")]
    [SerializeField] public float attackDistance;

    [Header("Move info")]
    public float idleTime;
    #region
    public EnermyStateMachine StateMachine { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnermyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
    }
    public void AnimationTrigger()
    {
        StateMachine.currentState.AnimationFinishTrigger();
    }
    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, WhatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
