using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int ComboCounter = 0;
    private float lastTimeAttack;
    private float ComboWindows = 0.2f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (ComboCounter>2 || Time.time >= lastTimeAttack + ComboWindows)
        {
            ComboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", ComboCounter);
        player.anim.speed = 1f;//用来控制动画的速度，可以用在武器系统，有些武器增加攻速，有些武器减少攻速。

        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        player.SetVelocity(player.attackMovement[ComboCounter].x * attackDir, player.attackMovement[ComboCounter].y);//攻击时附带的移动

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        ComboCounter++;
        lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0){
            player.zeroVelocity();
        }
        if (triggerCalled) { 
            stateMachine.ChangeState(player.idleState);
        }
    }
}
