using UnityEngine;
using System;
using System.Dynamic;
using Unity.VisualScripting;
using Unity.Mathematics;

public class Player : MonoBehaviour
{
    //air speed = walkspeeed && sprintspeed 
    [Header("Move")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;


    public float speedTransitionRate = 2f;
    public float fovTransitionRate = 8f;
    public float moveSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundDistance = 0.2f;

    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Fov Modifier")]
    public Camera normalCam;
    protected float baseFov = 60;
    protected float sprintFovModifier = 1.3f;

    public Transform orientation;
    protected float horizontalInput;
    protected float verticalInput;

    Vector3 moveDirection;
    public float gravity = -9.81f;

    protected Rigidbody rb;

    public MovementState state; // always stores the current state the player is in

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //freeze x y 
        baseFov = normalCam.fieldOfView; //sets the baseFov to the normalCam fieldOfView from the Camera
        readyToJump = true;
    }

    void Update()
    {
        SpeedControl();
        StateHandler();
        MyInput();

        //Ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        Console.WriteLine("Grounded: " + grounded);
        Console.WriteLine("not Grounded: " + !grounded);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Gravity();
    }

    protected void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    protected void StateHandler()
    {
        // Set the appropriate state
        if (Input.GetKey(sprintKey) && verticalInput > 0)
        {
            state = MovementState.sprinting;
            // Smoothly increase speed from walkSpeed to sprintSpeed
            moveSpeed = Mathf.MoveTowards(moveSpeed, sprintSpeed, Time.deltaTime * speedTransitionRate);
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }

        adjustFOV();
    }

    private void adjustFOV()
    {
        //Field of view
        if (state == MovementState.sprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFov * sprintFovModifier, Time.deltaTime * fovTransitionRate); }
        else { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFov, Time.deltaTime * fovTransitionRate); }
    }

    protected void Gravity()
    {
        rb.AddForce(0, gravity, 0);
    }

    protected void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //moveDirection.normalized

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //moveDirection.normalized


    }

    protected void SpeedControl()
    {
        // limiting speed on ground or air
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    protected void Jump()
    {
        Vector3 vel = rb.linearVelocity;
        vel.y = 0;
        rb.linearVelocity = vel;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    protected void ResetJump()
    {
        readyToJump = true;
    }
}