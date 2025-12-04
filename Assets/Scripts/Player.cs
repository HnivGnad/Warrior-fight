using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private StateMachine stateMachine;
    public PlayerInputSet input { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }

    [Header("Attack detail")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = 0.1f;
    public int comboResetTime = 1;
    private Coroutine queueAttackCo;
    public Vector2 jumpAttackVelocity;

    [Header("Movement detail")]
    public float moveSpeed;
    private bool facingRight = true;
    public float jumpForce = 5f;
    public Vector2 moveInput { get; private set; }

    public int facingDir { get; private set; } = 1;
    [Range(0, 1)]
    public float wallSlideSlowMultiplier;
    public float dashDuration { get; private set; } = 0.25f;
    public float dashSpeed { get; private set; } = 15f;

    [Header("Collision detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    public bool groundDetect { get; private set; }
    public LayerMask whatIsLayout;
    public bool wallDetect;
    public Vector2 wallJumpForce;
    public Transform primaryWallCheck;
    public Transform secondaryWallCheck;


    [Range(0, 1)]
    public float inAirMoveMutiplier = 0.7f;
    
    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }
    private void OnDisable() {
        
    }
    void Start()
    {
        stateMachine.Initialize(idleState);
    }
    void Update()
    {
        stateMachine.UpdateActiveState();
        HandleCollisionDetect();
    }
    public void EnterAttackStateWithDelay() {
        if (queueAttackCo != null)
            StopCoroutine(queueAttackCo);

        queueAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
        
    }
    private IEnumerator EnterAttackStateWithDelayCo() {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }
    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    private void HandleFlip(float xVelocity) {
        if (xVelocity > 0 && facingRight == false)
            Flip();
        else if(xVelocity < 0 && facingRight)
            Flip();
    }
    public void Flip() {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }
    public void CallAnimationTrigger() {
        stateMachine.currentState.CallAnimationTrigger();
    }
    private void HandleCollisionDetect() {
        groundDetect = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsLayout);
        wallDetect = Physics2D.Raycast(primaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout)
                  && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
