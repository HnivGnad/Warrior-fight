using UnityEngine;

public class Player_BasicAttackState : PlayerState {

    private float attackVelocityTimer;
    private float lastTimeAttack;
    public int attackDir;
    private bool comboAttackQueued;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {
        if (comboLimit != player.attackVelocity.Length) {
            Debug.LogWarning("Dieu chinh combo litmit phu hop voi mang attack velocity");
            comboLimit = player.attackVelocity.Length;
        }
    }
    override public void Enter() {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        //tan cong theo huong dau vao moveInput.
        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }



    public override void Update() {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();

        if (triggerCall)
            HandleStateExit();
    }

    private void HandleStateExit() {
        if (comboAttackQueued) {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit() {
        base.Exit();
        comboIndex++;
        lastTimeAttack = Time.time;
    }

    private void QueueNextAttack() {
        if (comboIndex < comboLimit) {
            comboAttackQueued = true;
        }
    }

    private void HandleAttackVelocity() {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);
    }
    private void ApplyAttackVelocity() {
        attackVelocityTimer = player.attackVelocityDuration;
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);

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
