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
	
	
	private void Awake()
	{
		PlayerTouchHandler.OnPlayerJump += Jump;
		PlayerTouchHandler.OnPlayerStopJump += StopJump;
		
		GetPlayerComponents();
		
		maxGravityVelocity *= -1;
	}
	private void Update()
	{
		isGrounded = Physics.Raycast(transform.position,Vector3.down,2 * .5f + .2f, iAmGround);
		
		if(isGrounded)
		{
			ResetJump();
		}
	}
	
	private void FixedUpdate()
	{
		PlayerGravity();
	}
	
	private void GetPlayerComponents()
	{
		rb = GetComponent<Rigidbody>();
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
}
