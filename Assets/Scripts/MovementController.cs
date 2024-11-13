using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float Speed;
	public Transform Cam;

	private CharacterController controller;
	private float playerSpeed = 5.0f;
	private bool groundedPlayer;
	private Vector3 playerVelocity;
	private float jumpHeight = .0f;
	private float gravityValue = -9.81f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

		if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;

			transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraController>().sensivity * Time.deltaTime);

			Quaternion CamRotation = Cam.rotation;
			CamRotation.x = 0f;
			CamRotation.z = 0f;

			transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
		}

		groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
	}
}
