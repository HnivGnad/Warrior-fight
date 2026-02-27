using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    public float damage = 10;

    [Header("target details")]
    [SerializeField] Transform targetCheck;
    [SerializeField] float targetRadius = 1 ;
    [SerializeField] LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }
    public void PerformAttack() {
        GetDetectCollider();

        foreach (var target in GetDetectCollider()) {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if(damagable == null) continue;

            damagable.TakeDamage(damage, transform);
            vfx.CreateOnHitVfx(target.transform);
            
        }
    }
    protected Collider2D[] GetDetectCollider() {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetRadius, whatIsTarget);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetRadius);
    }
}
