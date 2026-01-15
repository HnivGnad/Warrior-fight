using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_DeathState : EnemyState {

    public Enemy_DeathState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName) {
    }

    public override void Enter() {
        anim.SetTrigger("death");
        rb.linearVelocity = new Vector2(5, 3);

        stateMachine.SwitchOffStateMachine();
        
    }

}
