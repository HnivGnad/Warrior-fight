using Unity.VisualScripting;
using UnityEngine;

public class Enemy_MoveState : Enemy_GroudedState
{
    
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (enemy.groundDetect == false || enemy.wallDetect)
            enemy.Flip();
    }
    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if (enemy.groundDetect == false || enemy.wallDetect)
            stateMachine.ChangeState(enemy.idleState);
    
    }
}
