using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AiredState : EntityState {
    public Player_AiredState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }
    
    public override void Update() { 
        base.Update();
        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMutiplier), rb.velocity.y);
    }
}
