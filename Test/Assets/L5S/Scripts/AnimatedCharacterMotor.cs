using UnityEngine;
using System.Collections;

public class AnimatedCharacterMotor : MonoBehaviour 
{
	public bool IsJumping { get 
				{return 
					animatorController.GetCurrentAnimatorStateInfo(0).IsName("jumping");
					} }
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
	private Animator animatorController;
	private int walkingSpeedHashId;
	private int jumpTriggerHashId;
	
	// add a small delay between jumps while walking, 
	// so that the transition between
	// the two animations can play while the character
	// hits the ground from a previous jump:
	private float jumpDelay = 0.125f;
	private float maxJumpDelay = 0.125f; 
	private bool wasGroundedAfterJump;




	void Awake()
	{
		characterController = GetComponent<CharacterController>();

		animatorController = GetComponent<Animator>();
		walkingSpeedHashId = Animator.StringToHash("walkingSpeed");
		jumpTriggerHashId  = Animator.StringToHash("jump");
	}

	void Update() 
	{

	    // if we are on the ground then allow movement
	    if (IsGrounded) 
	    {
			float input = Input.GetAxis("Vertical");
	        bool  isMoving = (input != 0);

			wasGroundedAfterJump = true;

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

			animatorController.SetFloat(walkingSpeedHashId, Mathf.Abs(currentSpeed));

	        
			if (Input.GetButton("Jump") && !IsJumping) 
			{
				if (Mathf.Abs(currentSpeed) < 0.1)
					StartCoroutine(JumpInPlace());
				else if (jumpDelay <= 0.0125f)
				{
					animatorController.SetTrigger(jumpTriggerHashId);
					moveDirection.y = jumpSpeed;
				}
			}
	    }
		else
		{
			moveDirection.y -= (gravity * Time.deltaTime);
			if (moveDirection.y > 0.0125f)
			{
				jumpDelay = maxJumpDelay;
				wasGroundedAfterJump = false;
			}
		}
		float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

		if (wasGroundedAfterJump)
		{
			jumpDelay -= Time.deltaTime;
			jumpDelay  = Mathf.Max(0, jumpDelay);
		}

	    transform.Rotate(0, rotation, 0);

		characterController.Move(moveDirection * Time.deltaTime);
	}


	IEnumerator JumpInPlace ()
	{
		animatorController.SetTrigger(jumpTriggerHashId);
		// wait for jumping animation to reach the point where the character
		// actually jumps off the ground:
		while (!IsJumping
			|| animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.25f)
			yield return null;
		moveDirection.y = jumpSpeed;
		
		yield return new WaitForSeconds(1.0f);
	}
	
}
