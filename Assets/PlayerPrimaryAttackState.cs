using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int ComboCounter = 0;
    private float lastTimeAttack;
    private float ComboWindows = 2;

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
        player.anim.speed = 1.3f;//�������ƶ������ٶȣ�������������ϵͳ����Щ�������ӹ��٣���Щ�������ٹ��١�
        player.SetVelocity(player.attackMovement[ComboCounter].x * player.facingDir, player.attackMovement[ComboCounter].y);//����ʱ�������ƶ�

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
            rb.linearVelocity = new Vector2(0, 0);
        }
        if (triggerCalled) { 
            stateMachine.ChangeState(player.idleState);
        }
    }
}
