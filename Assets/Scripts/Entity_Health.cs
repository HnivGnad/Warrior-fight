using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    protected Entity_VFX entity_VFX;
    protected Entity entity;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;
    [SerializeField] float currentHp;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(3.5f, 3.5f);
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = .3f;


    protected virtual void Awake() {
        entity_VFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer) {
        if(isDead) 
            return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity_VFX?.PlayOnDamageVFX();

        entity?.ReciveKnockback(knockback, duration);

        ReduceHp(damage);
    }

    public virtual void ReduceHp(float damage) {
        currentHp -= damage;

        if (currentHp <= 0)
            Die();
    }

    private void Die() {
        isDead = true;
        entity.EntityDeath();
    }
    public Vector2 CalculateKnockback(float damage, Transform damageDealer) {
        int diretion = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x = knockback.x * diretion;

        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshold;
}
