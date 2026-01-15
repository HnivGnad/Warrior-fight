using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected StateMachine stateMachine;


    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    public bool groundDetect { get; private set; }
    public LayerMask whatIsGround;
    public bool wallDetect;
    public Vector2 wallJumpForce;
    public Transform primaryWallCheck;
    public Transform secondaryWallCheck;


    [Range(0, 1)]
    public float inAirMoveMutiplier = 0.7f;

    //Condition variable
    private bool isKnocked;
    private Coroutine knockbackCo;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();

    }
    protected virtual void Start() {
        
    }
    protected virtual void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisionDetect();
    }
    public void ReciveKnockback(Vector2 knockback, float duration) {
        if(knockbackCo != null)
            StopCoroutine(knockbackCo);

        knockbackCo = StartCoroutine(KnockBackCo(knockback, duration));
    }

    private IEnumerator KnockBackCo(Vector2 knockback, float duration) {
        isKnocked = true;
        rb.linearVelocity = knockback;

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        isKnocked = false; 
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;

        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    public void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingRight == false)
            Flip();
        else if (xVelocity < 0 && facingRight)
            Flip();
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }
    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }
    public virtual void EntityDeath() {

    }

    private void HandleCollisionDetect()
    {
        groundDetect = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (secondaryWallCheck != null)
        {
            wallDetect = Physics2D.Raycast(primaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround)
                      && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
        }
        else
        {
            wallDetect = Physics2D.Raycast(primaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance));
        if (secondaryWallCheck != null)
        {
            Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
            Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        }
        else
        {
            Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        }
    }
}
