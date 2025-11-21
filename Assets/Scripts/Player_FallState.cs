using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : Player_AiredState {
    public Player_FallState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
    }
    public override void Enter() {
        base.Enter();
    }
    public override void Update() { 
        base.Update();
        if (player.groundDetect)
            stateMachine.ChangeState(player.idleState);
    }
}
