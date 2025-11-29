using UnityEngine;

public class Player_WallSlideState : EntityState {
    public Player_WallSlideState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }
    public override void Update() {
        base.Update();

        if (player.wallDetect == false) {
            stateMachine.ChangeState(player.fallState);
        }

        HandleWallSlide();
        if (player.groundDetect) {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }

    }
    private void HandleWallSlide() {
        if (player.moveInput.y < 0 && player.groundDetect == false) {
            player.SetVelocity(player.moveInput.x, rb.linearVelocityY);
        }
        else {
            player.SetVelocity(player.moveInput.x, rb.linearVelocityY * player.wallSlideSlowMultiplier);
        }
    }
}
