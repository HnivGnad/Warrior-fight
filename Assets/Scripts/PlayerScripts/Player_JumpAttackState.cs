using UnityEngine;

public class Player_JumpAttackState : PlayerState {
    private bool touchGround;

    public Player_JumpAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }
    public override void Enter() {
        base.Enter();
        touchGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }
    public override void Update() {
        base.Update();
        if (player.groundDetect && touchGround == false) {
            touchGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocityY);
        }
        if (triggerCall && player.groundDetect) {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
