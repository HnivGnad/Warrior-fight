using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamagable
{
    public Slider healthBar;

    protected Entity_VFX entity_VFX;
    protected Entity entity;
    private Entity_Stats stats;

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
        stats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }

    public virtual bool TakeDamage(float damage, Transform damageDealer) {
        if(isDead) 
            return false;

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack!");
            return false;
        }

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity_VFX?.PlayOnDamageVFX();

        entity?.ReciveKnockback(knockback, duration);

        ReduceHp(damage);

        return true;
    }
    private bool AttackEvaded() => Random.Range(0, 100) < stats.GetEvasion();
    

    public virtual void ReduceHp(float damage) {
        currentHp -= damage;

        if (currentHp <= 0)
            Die();

        UpdateHealthBar();
    }

    private void Die() {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;
        healthBar.value = currentHp / stats.GetMaxHealth();
    }

    public Vector2 CalculateKnockback(float damage, Transform damageDealer) {
        int diretion = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x = knockback.x * diretion;

        return knockback;
    }
    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshold;
}
