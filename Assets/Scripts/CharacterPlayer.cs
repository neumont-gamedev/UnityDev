using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CharacterPlayer;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour
{
	public enum Mode
	{
		THIRD_PERSON,
		FREE
	}

	[SerializeField] private PlayerData playerData;
	[SerializeField] private Animator animator;
	[SerializeField] private InputRouter inputRouter;
	[SerializeField] private Inventory inventory;

	//[SerializeField] private CharacterCamera characterCamera;
	[SerializeField] private Mode mode = Mode.THIRD_PERSON;

	CharacterController characterController;
	Vector2 inputAxis;

	Camera mainCamera;
	Vector3 velocity = Vector3.zero;
	float inAirTime = 0;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		mainCamera = Camera.main;

		inputRouter.jumpEvent += OnJump;
		inputRouter.moveEvent += OnMove;
		inputRouter.fireEvent += OnFire;
		inputRouter.fireStopEvent += OnFireStop;
		inputRouter.nextItemEvent += OnNextItem;

		GetComponent<Health>().onDeath += OnDeath;
	}

	void Update()
	{
		Vector3 direction = Vector3.zero;
		direction.x = inputAxis.x;
		direction.z = inputAxis.y;

		switch (mode)
		{
			case Mode.THIRD_PERSON:
				// convert direction to character space
				direction = transform.rotation * direction;
				break;
			case Mode.FREE:
				// convert direction to camera space
				// convert the camera yaw to a quaternion (rotation)
				Quaternion q = Quaternion.AngleAxis(mainCamera.transform.eulerAngles.y, Vector3.up);
				// set the direction to be in camera space
				direction = q * direction;
				break;
		}

		if (characterController.isGrounded)
		{
			velocity.x = direction.x * playerData.speed;
			velocity.z = direction.z * playerData.speed;
			inAirTime = 0;
		}
		else
		{
			inAirTime += Time.deltaTime;
			velocity.y += playerData.gravity * Time.deltaTime;
		}

		characterController.Move(velocity * Time.deltaTime);
		
		// rotation
		// set the look vector to the direction vector but ignore the y component (up/down)
		Vector3 look = direction;
		look.y = 0;
		// if the player is moving (look vector length is greater than 0) update the rotation
		if (look.magnitude > 0)
		{
			switch (mode)
			{
				case Mode.THIRD_PERSON:
					// rotate with input axis x (horizontal)
					transform.rotation *= Quaternion.AngleAxis(inputAxis.x * playerData.turnRate * Time.deltaTime, Vector3.up);
					
					break;
				case Mode.FREE:
					// rotate towards look at direction
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
					break;
			}
		}
	
		// set animator parameters
		Vector3 v = velocity;
		v.y = 0;
		animator.SetFloat("Speed", v.magnitude);
		animator.SetFloat("VelocityY", characterController.velocity.y);
		animator.SetFloat("InAirTime", inAirTime);
		animator.SetBool("IsGrounded", characterController.isGrounded);
	}

	//	transform.rotation *= Quaternion.AngleAxis(direction.x * 2, Vector3.up);
	//	//transform.rotation = Quaternion.Euler(0, followTargetTransform.rotation.eulerAngles.y, 0);
	//	//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);


	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;

		// no rigidbody
		if (body == null || body.isKinematic)
		{
			return;
		}

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3)
		{
			return;
		}

		// Calculate push direction from move direction,
		// we only push objects to the sides never up and down
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.

		// Apply the push
		body.velocity = pushDir * playerData.hitForce;
	}

	public void OnJump()
	{
		if (characterController.isGrounded)
		{
			animator.SetTrigger("Jump");
			velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);
		}
	}

	public void OnFire()
	{
		inventory.Use();
	}

	public void OnFireStop()
	{
		inventory.StopUse();
	}

	public void OnNextItem()
	{
		inventory.EquipNextItem();
	}

	public void OnMove(Vector2 axis)
	{
		inputAxis = axis;
	}

	public void OnAnimEventItemUse()
	{
		inventory.OnAnimEventItemUse();
	}

	public void OnLeftFootSpawn(GameObject go)
	{
		Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
		Instantiate(go, bone.position, bone.rotation);
	}

	public void OnRightFootSpawn(GameObject go)
	{
		Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
		Instantiate(go, bone.position, bone.rotation);
	}

	public void OnDeath()
	{
		Debug.Log("player dead");
	}
}
