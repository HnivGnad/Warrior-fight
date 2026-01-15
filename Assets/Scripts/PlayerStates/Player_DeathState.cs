using UnityEngine;

public class Player_DeathState : PlayerState {
    public Player_DeathState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }

    public override void Enter() {
        base.Enter();

        rb.linearVelocity = new Vector2(5, 3);
        input.Disable();

        rb.simulated = false;
    }
}
