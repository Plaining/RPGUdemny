using UnityEngine;

public class EnermyBattleState : EnermygGroundState
{
    private Transform player;
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
            if (enermy.isPlayerDetected().distance < enermy.attackDistance)
            {
                enermy.zeroVelocity();
                //stateMachine.ChangeState(enermy.AttackState);
                return;
            }
        }

        if (player.position.x > enermy.transform.position.x)
        {
            moveDir = 1;
        }else if(player.position.x < enermy.transform.position.x)
        {
            moveDir = -1;
        }
        enermy.SetVelocity(enermy.moveSpeed * moveDir, rb.linearVelocity.y);

       
    }
}
