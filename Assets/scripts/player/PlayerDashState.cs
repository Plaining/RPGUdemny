using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.clone.CreateClone(player.transform);
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(!player.isGroundDetected() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
