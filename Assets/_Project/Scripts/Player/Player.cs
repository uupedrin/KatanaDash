using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Requires PlayerTouchHandler
[RequireComponent(typeof(PlayerTouchHandler))]
public class Player : MonoBehaviour
{
	CharacterController charController;
	Vector3 playerMovement = Vector3.zero;
	
	//gravity
	bool playerGrounded;
	float gravity = -9.8f;
	float groundedGravity = -.05f;
	bool useGravity = true;
	
	//jumping
	[Header("Jumping")]
	[SerializeField]
	float maxJumpHeight = 4.0f;
	[SerializeField]
	float maxJumpTime = .75f;
	bool isJumpPressed = false;
	float initialJumpVelocity;
	bool isJumping = false;
	bool canJump = true;
	
	//dashing
	[Header("Dashing")]
	[SerializeField]
	float dashSpeed = 20f;
	[SerializeField]
	float dashDuration = .7f;
	bool isDashing = false;
	bool canDash = true;
	Coroutine DashCoroutine = null;
	
	//health
	public int health;

	void Awake()
	{
		SubscribeToEvents();
		GetPlayerComponents();
		
		SetupJumpVariables();
	}
	void Update()
	{
		RunRaycasts();
		
		charController.Move(playerMovement * Time.deltaTime);
		HandleGravity();
		HandleJump();
	}
	
	void SubscribeToEvents()
	{
		PlayerTouchHandler.OnPlayerJump += OnJump;
		PlayerTouchHandler.OnPlayerDash += OnDash;
	}
	void GetPlayerComponents()
	{
		charController = GetComponent<CharacterController>();
	}
	void RunRaycasts()
	{
		playerGrounded = charController.isGrounded;
	}
	
	void HandleGravity()
	{
		if(!useGravity) return;
		
		bool isFalling = playerMovement.y <= 0.0f || !isJumpPressed;
		float fallMultiplier = 2.0f;
		if(playerGrounded)
		{
			playerMovement.y = groundedGravity;
		}
		else if(isFalling)
		{
			float previousYVelocity = playerMovement.y;
			float newYVelocity = playerMovement.y + (gravity * fallMultiplier * Time.deltaTime);
			float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
			playerMovement.y = nextYVelocity;
		}
		else 
		{
			float previousYVelocity = playerMovement.y;
			float newYVelocity = playerMovement.y + (gravity * Time.deltaTime);
			float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
			playerMovement.y = nextYVelocity;
		}
	}
	
	void OnJump(bool value)
	{
		isJumpPressed = value;
	}
	void HandleJump()
	{
		if(!isJumping && playerGrounded && isJumpPressed) //Change to can jump
		{
			isJumping = true;
			playerMovement.y = initialJumpVelocity * .5f;
		}
		else if(!isJumpPressed && isJumping && playerGrounded)
		{
			isJumping = false;
		}
	}
	void SetupJumpVariables()
	{
		float timeToApex = maxJumpTime / 2;
		gravity = -2 * maxJumpHeight / Mathf.Pow(timeToApex, 2);
		initialJumpVelocity = 2 * maxJumpHeight / timeToApex;
	}
	
	void OnDash()
	{
		if(canDash && DashCoroutine == null)
		{
			DashCoroutine = StartCoroutine(Dash());
		}
	}
	IEnumerator Dash()
	{
		useGravity = false;
		isDashing = true;
		float initialXVelocity = playerMovement.x;
		playerMovement.x += dashSpeed;
		playerMovement.y = 0;
		yield return new WaitForSeconds(dashDuration);
		playerMovement.x = initialXVelocity;
		isDashing = false;
		useGravity = true;
		DashCoroutine = null;
	}
	public bool IsDashing
	{
		get{return isDashing;}
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(!isDashing && hit.gameObject.tag == "Enemy") 
		{
			//add feedback
			//resetar fase
		}
		else if(isDashing && hit.gameObject.tag == "Enemy") 
		{
			Destroy(hit.gameObject);
		}
	}
}
