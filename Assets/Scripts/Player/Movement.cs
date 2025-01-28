using UnityEngine;
using System;
using System.Dynamic;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;

public class Movement : MonoBehaviour
{

    [Header("Move")]
    public float walkSpeed;
    public float sprintSpeed;
    public float airSpeed; // air movement might add a nice feel to the game?
    public float targetAirSpeed;


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
    public Transform groundCheck;
    public float groundDistance = 0.2f;

    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Fov Modifier")]
    public Camera normalCam;
    protected float BaseFov = 60;
    protected float SprintFovModifier = 1.3f;

    public Transform orientation;
    protected float HorizontalInput;
    protected float VerticalInput;

    private Vector3 _moveDirection;
    public float gravity = -9.81f;

    private Rigidbody _rb;

    public MovementState state; // always stores the current state the player is in


    public void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true; //freeze x y

        BaseFov = normalCam.fieldOfView; //sets the baseFov to the normalCam fieldOfView from the Camera

        readyToJump = true;
    }

    void Update()
    {
        SpeedControl();
        StateHandler();
        ProcessJumpInput();
        Zoom();

        //Ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        // handle drag
        if (grounded)
        {
            _rb.linearDamping = groundDrag;
        }
        else
        {
            _rb.linearDamping = 0;
        }

        Console.WriteLine("Grounded: " + grounded);
        Console.WriteLine("not Grounded: " + !grounded);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Gravity();
    }

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    protected void ProcessJumpInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    protected void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey) && VerticalInput > 0)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - walking
        else if (grounded) //if player is grounded but not pressing sprint set state to walkSpeed
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else // if player is not grounded and not pressing sprint set state to air
        {
            state = MovementState.air;
            moveSpeed = Mathf.Lerp(moveSpeed, targetAirSpeed, Time.deltaTime * 3f);
            moveSpeed = airSpeed;
        }

        //Field of view
        if (state == MovementState.sprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFov * SprintFovModifier, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFov, Time.deltaTime * 8f);
        }
    }

    protected void Gravity()
    {
        _rb.AddForce(0, gravity, 0);
    }

    protected void MovePlayer()
    {
        // calculate movement direction
        _moveDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;

        // on ground
        if (grounded)
        {
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //moveDirection.normalized
        }
        else // in air
        {
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //moveDirection.normalized
        }
    }

    protected void SpeedControl()
    {
        // limiting speed on ground or air
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        }
    }

    protected void Jump()
    {
        Vector3 vel = _rb.linearVelocity;
        vel.y = 0;
        _rb.linearVelocity = vel;
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    protected void ResetJump()
    {
        readyToJump = true;
    }

    protected void Zoom()
    {
        if (Input.GetMouseButton(1))
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFov / 5f, Time.deltaTime * 1f);
        }
        else
        {
            if (state == MovementState.sprinting)
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFov * SprintFovModifier, Time.deltaTime * 1f);
            }
            else
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, BaseFov, Time.deltaTime * 1f);
            }
        }
    }
}
