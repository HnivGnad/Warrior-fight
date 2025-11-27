using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleSate : Player_GroundedState {
    public Player_IdleSate(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }
    public override void Enter() {
        base.Enter();
        player.SetVelocity(0, rb.linearVelocity.y);
    }
    public override void Update() {
        base.Update();
        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);

    }
}
