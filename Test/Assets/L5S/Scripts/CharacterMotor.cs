using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour 
{
	public bool IsGrounded { get {return characterController.isGrounded;}}

	public float speed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
	public float rotationSpeed = 100.0f;

	public float currentSpeed = 0.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 10.0f;
	public float decceleration = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController characterController;




	void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	void Update() 
	{

	    // if we are on the ground then allow movement
	    if (IsGrounded) 
	    {
			float input = Input.GetAxis("Vertical");
	        bool  isMoving = (input != 0);

			moveDirection.x = transform.forward.x;
			moveDirection.z = transform.forward.z;
	        
			if (isMoving) 
			{
				currentSpeed += ((acceleration * input) * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
			}
			else if (currentSpeed > 0) 
			{
				currentSpeed -= (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
			}
			else if (currentSpeed < 0) 
			{
				currentSpeed += (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, 0);
			}

			moveDirection.x *= currentSpeed;
			moveDirection.z *= currentSpeed;

			moveDirection.y  = Mathf.Max(0, moveDirection.y);

	        
			if (Input.GetButton("Jump")) 
			{
				moveDirection.y = jumpSpeed;
			}
	    }
		else
		{
			moveDirection.y -= (gravity * Time.deltaTime);
		}
		float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

	    transform.Rotate(0, rotation, 0);

		characterController.Move(moveDirection * Time.deltaTime);
	}
	
}
