using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires PlayerTouchHandler
[RequireComponent(typeof(PlayerTouchHandler))]
public class Player : MonoBehaviour
{
	private Rigidbody rb;
	
	[Header("Gravity")]
	[SerializeField]
	private float maxGravityVelocity;
	[SerializeField]
	private float gravityIncreaseValue;
	
	
	[Header("Ground Check")]
	[SerializeField]
	private LayerMask iAmGround;
	private bool isGrounded;
	
	
	[Header("Jump System")]
	[SerializeField]
	private float jumpForce = 0f;
	[SerializeField]
	private float maxJumpTime;
	private float jumpTimer = 0f;
	private bool canJump = true;
	private bool isJumping = false;
	
	
	[Header("Dash System")]
	[SerializeField]
	private LayerMask iAmDashable;
	[SerializeField]
	private float dashDistance;
	[SerializeField]
	private float dashSpeed;
	private RaycastHit[] dashableObjects;
	private bool canDash = true;
	private bool isDashing = false;
	private Vector3 dashFinalPos;
	
	
	
	
	private void Awake()
	{
		SubscribeToActions();
		GetPlayerComponents();
		
		maxGravityVelocity *= -1;
	}
	
	private void Update()
	{
		
		RunRaycasts();
				
		if(isGrounded)
		{
			ResetJump();
			ResetDash();
		}
			
		ExecuteDash();
	}
	
	private void FixedUpdate()
	{
		PlayerGravity();
	}
	
	
	private void GetPlayerComponents()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	private void SubscribeToActions()
	{
		PlayerTouchHandler.OnPlayerJump += Jump;
		PlayerTouchHandler.OnPlayerStopJump += StopJump;
		PlayerTouchHandler.OnPlayerDash += Dash;
	}
	
	private void RunRaycasts()
	{
		isGrounded = Physics.Raycast(transform.position,Vector3.down,2 * .5f + .2f, iAmGround);
		dashableObjects = Physics.RaycastAll(transform.position, transform.forward ,dashDistance, iAmDashable);
	}
	
	
	private void PlayerGravity()
	{
		float yForce;
		
		if(isJumping)
		{
			jumpTimer += Time.deltaTime;
			if(jumpTimer >= maxJumpTime)
			{
				StopJump();
			}
			yForce = Mathf.Clamp(rb.velocity.y, maxGravityVelocity/5f, 20f);
		}
		else
		{
			yForce = Mathf.Clamp(rb.velocity.y, maxGravityVelocity, 2f);
		}
		
		rb.velocity = new Vector3(rb.velocity.x, yForce, rb.velocity.z);
		
	}
	
	private void Jump()
	{
		if(canJump)
		{
			isDashing = false; //Cancels Dash if Jump
			
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			canJump = false;
			isJumping = true;
		}
	}
	
	private void ResetJump()
	{
		canJump = true;
		jumpTimer = 0f;
	}
	private void StopJump()
	{
		isJumping = false;
	}
	
	
	
	private void Dash()
	{
		if(canDash && !isDashing)
		{
			StopJump(); //Cancels Jump if Dash
			
			isDashing = true;
			dashFinalPos = new Vector3(transform.position.x + dashDistance, transform.position.y,transform.position.z);
		}
	}
	
	private void ExecuteDash()
	{
		if(!isDashing) return;
		if(transform.position.x >= dashFinalPos.x) isDashing = false;
		
		transform.position = Vector3.MoveTowards(transform.position, dashFinalPos, dashSpeed);
	}
	
	private void ResetDash()
	{
		canDash = true;
	}
}
