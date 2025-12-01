using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private const int FirstComboIndex = 1;
    private float attackVelocityTimer;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private float lastTimeAttack;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        if (comboLimit != player.attackVelocity.Length) {
            Debug.LogWarning("Do dai combo litmit = attackVelocity");
            comboLimit = player.attackVelocity.Length;
        }
    }
    override public void Enter() {
        base.Enter();
        ResetComboIndexIfNeeded();
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }

    

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (triggerCall)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }

    public override void Exit() {
        base.Exit();
        comboIndex++;
        lastTimeAttack = Time.time;
    }
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }
    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);

    }

    private void ResetComboIndexIfNeeded() {
        if (Time.time > lastTimeAttack + player.comboResetTime) {
            comboIndex = FirstComboIndex;
        }
        if (comboIndex > comboLimit) {
            comboIndex = FirstComboIndex;
        }
    }
}
