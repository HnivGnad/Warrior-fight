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
    private void HandleCollisionDetect() {
        groundDetect = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsLayout);
        wallDetect = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance * facingDir, whatIsLayout);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
