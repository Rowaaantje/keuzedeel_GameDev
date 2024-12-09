using UnityEngine;
using System;
using System.Dynamic;
using Unity.VisualScripting;
using Unity.Mathematics;

public class Movement : MonoBehaviour
{

    [Header("Move")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float airSpeed; // air movement might add a nice feel to the game?
    [SerializeField] float targetAirSpeed;


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


    public void Start() {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //freeze x y

        baseFov = normalCam.fieldOfView; //sets the baseFov to the normalCam fieldOfView from the Camera

        readyToJump = true;
    }
        [Obsolete]
    void Update() {

        SpeedControl();
        StateHandler();
        MyInput();
        //Ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

         // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.drag = 0;

        Console.WriteLine("Grounded: " + grounded);
        Console.WriteLine("not Grounded: " + !grounded);
    }

    private void FixedUpdate(){
        MovePlayer();
        Gravity();
    }

    public enum MovementState {
        walking,
        sprinting,
        air
    }

    protected void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

     protected void StateHandler()
    {


        if (grounded && Input.GetKey(sprintKey) && verticalInput > 0)
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
        if(state == MovementState.sprinting) { normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFov * sprintFovModifier, Time.deltaTime * 8f); }
        else {normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFov, Time.deltaTime * 8f); }
    }

    protected void Gravity() {
        rb.AddForce(0, gravity, 0);
    }

    protected void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //moveDirection.normalized

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //moveDirection.normalized


    }

    protected void SpeedControl() {
        // limiting speed on ground or air
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    protected void Jump() {
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
