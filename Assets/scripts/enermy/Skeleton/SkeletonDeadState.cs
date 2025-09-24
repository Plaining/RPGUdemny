using UnityEngine;

public class SkeletonDeadState : EnermyState
{
    private Enermy_Skeleton enemy;
    public SkeletonDeadState(Enemy _enermy, EnermyStateMachine _stateMachine, string _animBoolName, Enermy_Skeleton _s
        ) : base(_enermy, _stateMachine, _animBoolName)
    {
        this.enemy = _s;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.lastAnimBoolname, false);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;
        stateTimer = .15f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer>0)
        {
            rb.linearVelocity = new Vector2(0, 10);
        }
    }
}
