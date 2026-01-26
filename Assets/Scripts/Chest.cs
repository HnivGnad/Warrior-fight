using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    [Header("OpenDetails")]
    [SerializeField] private Vector2 knockback = new Vector2(0, 2);

    public void TakeDamage(float damage, Transform damageDealer)
    {
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        fx.PlayOnDamageVFX();
    }

}
