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
    public LayerMask whatIsLayout;
    public bool wallDetect;
    public Vector2 wallJumpForce;
    public Transform primaryWallCheck;
    public Transform secondaryWallCheck;


    [Range(0, 1)]
    public float inAirMoveMutiplier = 0.7f;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();

    }


    protected virtual void Start()
    {

    }
    void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisionDetect();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    private void HandleFlip(float xVelocity)
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
    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }
    private void HandleCollisionDetect()
    {
        groundDetect = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsLayout);

        if (secondaryWallCheck != null)
        {
            wallDetect = Physics2D.Raycast(primaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout)
                      && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout);
        }
        else
        {
            wallDetect = Physics2D.Raycast(primaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout);
        }
    }
    private void OnDrawGizmos()
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
