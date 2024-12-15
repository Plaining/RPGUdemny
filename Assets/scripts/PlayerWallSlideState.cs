using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (yInput < 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else if (yInput >= 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY * player.wallSlideSpeed);
        }

    }
}
