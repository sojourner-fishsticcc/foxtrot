using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask groundCollisionMask;
    [SerializeField] private Rigidbody2D MercyRigidbody2D;
    public float runMax = 5f;           // the max speed in the x axis before it gets clamped
    public float crouchMax = 0.5f;        // the max speed in the x axis while crouching
    public float movementAccel = 8f;         // the rate at which the player accelerates to their desired velocity
    [SerializeField] public float jumpForce = 670f;      // numerical force applied when jumping
    public float horizontalRaw = 0f;    // stores the horizontal raw axis for other functions to access
    private bool isGrounded = false;    // updated by a raycast from the groundcheck empty
    private bool isCrouching = false;   // used for crouching and sliding
    private bool isAbleToSlide = false; // true when running and at max speed
    enum PlayerMoveState {Idle, Running, Crouching, Sliding, GrappleNullify}
    const float groundCheckRadius = .02f;
    const float ceilingCheckRadius = .02f;

    void Start()
    {

    }

    void Update()
    {
        ConstantPlayerChecks();
        if (isGrounded && Input.GetButton("Jump"))
        { Jumping(); }  // upwards impulse when player presses jump and is able to jump
        if (MercyRigidbody2D.velocity.y > 0 && Input.GetButtonUp("Jump") && !isGrounded)
        { JumpCut(); }  // cuts jump velocity if player releases jump
        if (5 > Mathf.Abs(MercyRigidbody2D.velocity.y) && !isGrounded)
        { JumpHold(); } // lowers gravity at peak if still holding jump
        if (MercyRigidbody2D.velocity.y < -5 && !isGrounded)
        { Falling(); }
        if (horizontalRaw == 0 && !isCrouching)
        { Idle(); }
            else if (horizontalRaw != 0 && !isCrouching)
            { Running(); }
        if (isGrounded && isCrouching && !isAbleToSlide)
        { Crouching(); }
            else if (isGrounded && isCrouching && isAbleToSlide)
            { Sliding(); }
    }
    public void Idle()
    {
        MovementHandler(float.PositiveInfinity, 1.05f);
        MercyRigidbody2D.gravityScale = 3;
    }
    public void Running()
    {
        MovementHandler(runMax, 1.05f);
        print("i am currently running");
    }       
    public void Crouching()
    {
        MovementHandler(crouchMax, 1.05f);
    }
    public void Sliding()
    {

    }
    public void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            MercyRigidbody2D.AddForce(new Vector2(0f,jumpForce));
        }
        if (Input.GetButton("Jump") && 3 < Mathf.Abs(MercyRigidbody2D.velocity.y))
        {
            MercyRigidbody2D.gravityScale = 2;
        }
    }
    public void JumpCut()
    {
        MercyRigidbody2D.velocity = new Vector2 (MercyRigidbody2D.velocity.x, MercyRigidbody2D.velocity.y / 2);
    }
    public void JumpHold()
    {
        if (Input.GetButton("Jump"))
        { MercyRigidbody2D.gravityScale = 2f; }
        else
        { MercyRigidbody2D.gravityScale = 3f; }
    }
    public void Falling()
    {
        MercyRigidbody2D.gravityScale = 7f;
    }
    public void GrappleNullify()
    {

    }
    public void ConstantPlayerChecks()
    {
        isGrounded = Physics2D.CircleCast(groundCheck.position, groundCheckRadius, groundCheck.position, groundCheckRadius, groundCollisionMask);
        horizontalRaw = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Crouch"))
        { isCrouching = true; }
        else if (Input.GetButtonUp("Crouch"))
        { isCrouching = false; }
    }
    public void MovementHandler(float runMaxMH, float FrictionCoeffMH)
    {
        MercyRigidbody2D.AddForce(new Vector2(horizontalRaw, 0f) * movementAccel);
        if (Mathf.Abs(MercyRigidbody2D.velocity.x) > runMaxMH)
            { MercyRigidbody2D.velocity = new Vector2 (Mathf.Sign(MercyRigidbody2D.velocity.x) * runMaxMH, MercyRigidbody2D.velocity.y); }
        if (horizontalRaw == 0 && isGrounded)
        { MercyRigidbody2D.velocity = new Vector2 (MercyRigidbody2D.velocity.x / FrictionCoeffMH, MercyRigidbody2D.velocity.y); }
    }
}
