using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;//每次攻击时，附带的移动属性
    public float counterAttackDuration = .2f;
    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float swordReturnImpact;
    public float moveSpeed = 3f;
    public float jumpForce = 10f;
    private float defaultMoveSpeed;
    private float defaultJumpForce;

    [Header("Dash info")]
    public float dashSpeed = 6.0f;
    public float dashDuration = 10.0f;
    private float defaultDashSpeed;
    public float dashDir { get; private set; }

    [Header("Slide info")]
    [SerializeField] public float wallSlideSpeed = 0.8f;
    public SkillManager skill {  get; private set; }
    public GameObject sword { get; private set; }
    #region Components
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
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine =  new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Jump");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            //skill.crystal.CanUseSkill();
        }
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);

        anim.speed = anim.speed * (1 - _slowPercentage);
        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void catchTheSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }

/*用来解决attack1，2，3之间有idle切换的问题。因为现在每次切换为idle时，如果xInput不是空，那么就会进入move状态机，这里加一段时间僵直*/
   
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    private void CheckForDashInput()
    {
        if (isWallDetected())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill() )
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }
 
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
