using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 3f;
    public float jumpForce = 10f;

    [Header("Collision info")]
    [SerializeField] private Transform groudCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;

    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }
    #endregion
    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine =  new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity * moveSpeed, _yVelocity);
    }

    public bool isGroundDetected() => Physics2D.Raycast(groudCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groudCheck.position, new Vector3(groudCheck.position.x, groudCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
