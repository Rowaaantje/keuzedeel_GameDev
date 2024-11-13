using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	public float moveSpeed;

	public float groundDrag;

	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump = true;

	[Header("Ground check")]
	public float playerHeight;
	public LayerMask groundMask;
	bool grounded;

	public Transform orientation;

	float horizontalInput;
	float verticalInput;
	Vector3 moveDirection;
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}

	void Update()
	{
		KeyboardInput();
		MovePlayer();
		ControlSpeed();

		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.5f, groundMask);
		rb.linearDamping = grounded ? groundDrag : 0f;
	}

	void FixedUpdate()
	{

	}

	private void KeyboardInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
		{
			readyToJump = false;

			Jump();

			Invoke(nameof(ResetJump), jumpCooldown);
		}
	}

	private void MovePlayer()
	{
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		if (grounded)
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
		else
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

	}

	private void ControlSpeed()
	{
		Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		if (flatVelocity.magnitude > moveSpeed)
		{
			Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
			rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
		}
	}

	private void Jump()
	{
		rb.linearVelocity = new Vector3( rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}

	private void ResetJump()
	{
		readyToJump = true;
	}
}
