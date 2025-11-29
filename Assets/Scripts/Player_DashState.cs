using UnityEngine;

public class Player_DashState : EntityState
{
    public float originGravityScale { get; private set; }
    public int dashDir { get; private set; }
    public Player_DashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        originGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        dashDir = player.facingDir;
    }
    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();

        player.SetVelocity(player.dashSpeed * dashDir, 0f);

        if (stateTimer < 0)
        {
            if (player.groundDetect)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0f, 0f);
        rb.gravityScale = originGravityScale;
    }
    private void CancelDashIfNeeded()
    {
        if (player.wallDetect)
        {
            if (player.groundDetect)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}

