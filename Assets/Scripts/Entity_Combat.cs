using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public float damage = 10;

    [Header("target details")]
    [SerializeField] Transform targetCheck;
    [SerializeField] float targetRadius = 1 ;
    [SerializeField] LayerMask whatIsTarget;

    public void PerformAttack() {
        GetDetectCollider();

        foreach (var target in GetDetectCollider()) {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>();
            targetHealth?.ReduceHp(damage);
            
        }
    }
    private Collider2D[] GetDetectCollider() {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetRadius, whatIsTarget);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetCheck.position, targetRadius);
    }
}
