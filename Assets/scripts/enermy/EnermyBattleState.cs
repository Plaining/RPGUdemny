using UnityEngine;

public class EnermyBattleState : EnermyState
{
    private Transform player;
    private Enermy_Skeleton enermy;
    private int moveDir;
    public EnermyBattleState(Enermy_Skeleton _enermy, EnermyStateMachine _stateMachine, string _animBoolName) : base(_enermy, _stateMachine, _animBoolName)
    {
        this.enermy = _enermy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("I see you");
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enermy.isPlayerDetected())
        {
            stateTimer = enermy.battleTime;
            if (enermy.isPlayerDetected().distance < enermy.attackDistance && canAttack())
            {
                /*Debug.Log("Change to attack");
                enermy.zeroVelocity();
                return;*/
                stateMachine.ChangeState(enermy.AttackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enermy.transform.position) > 7)
            {
                stateMachine.ChangeState(enermy.IdleState);
            }
        }
        /*���������*/
        if (player.position.x > enermy.transform.position.x)
        {
            moveDir = 1;
        }else if(player.position.x < enermy.transform.position.x)
        {
            moveDir = -1;
        }
        enermy.SetVelocity(enermy.moveSpeed * moveDir, rb.linearVelocity.y);

       
    }
    private bool canAttack()
    {
        if(Time.time >= enermy.lastTimeAttacked + enermy.attackCoolDown)
        {
            enermy.lastTimeAttacked = Time.time; 
            return true;
        }
        return false;
    }
}
