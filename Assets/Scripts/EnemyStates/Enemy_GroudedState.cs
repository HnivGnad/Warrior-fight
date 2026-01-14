using UnityEngine;

public class Enemy_GroudedState : EnemyState
{
    public Enemy_GroudedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        if(enemy.PlayerDetected())
        {
            stateMachine.ChangeState(enemy.battleState);
        }

    }
}
