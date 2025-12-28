using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;
        anim = enemy.anim;
        rb = enemy.rb;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMutiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("battleAnimSpeedMutiplier", battleAnimSpeedMutiplier);
        anim.SetFloat("moveAnimSpeedMutiplier", enemy.moveAnimSpeedMutiplier);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
