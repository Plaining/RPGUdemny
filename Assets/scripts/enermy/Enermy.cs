using UnityEngine;

public class Enermy : Entity
{
    [Header("Stunned info")]
    public float stunnedDuration;
    public Vector2 stunnedDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;/*在敌人头上提示可打击的时机*/

    [Header("PlayerCheck info")]
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] protected LayerMask WhatIsPlayer;
    [Header("Attack info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Move info")]
    public float idleTime;
    public float battleTime;
    public float moveSpeed;
    public float jumpForce;

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

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        else
        {
            return false;
        }
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
