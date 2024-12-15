using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;//每次攻击时，附带的移动属性

    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float moveSpeed = 3f;
    public float jumpForce = 10f;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed = 6.0f;
    public float dashDuration = 10.0f;
    public float dashDir { get; private set; }
    

    [Header("Collision info")]
    [SerializeField] private Transform groudCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;

    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    [Header("Slide info")]
    [SerializeField] public float wallSlideSpeed = 0.8f;

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
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine =  new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
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
        CheckForDashInput();
    }
/*用来解决attack1，2，3之间有idle切换的问题。因为现在每次切换为idle时，如果xInput不是空，那么就会进入move状态机，这里加一段时间僵直*/
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        Debug.Log("isBusy");
        yield return new WaitForSeconds(_seconds);
        Debug.Log("is not Busy");

        isBusy = false;
    }

    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            Debug.Log(dashDir);
            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity * moveSpeed, _yVelocity);
        FlipController(rb.linearVelocityX);
    }

    public bool isGroundDetected() => Physics2D.Raycast(groudCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, WhatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groudCheck.position, new Vector3(groudCheck.position.x, groudCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {//如果有向右的向量，且不是面向右边，需要flip
            Flip();
        }
        else if(_x < 0 && facingRight)//如果有向左的速度向量，且不是面向左边，需要Flip
        {
            Flip();
        }
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
