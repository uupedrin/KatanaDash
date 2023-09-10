using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody rb;
	
	[Header("Ground Check")]
	[SerializeField]
	private LayerMask iAmGround;
	private bool isGrounded;
	
	[Header("Player Jump")]
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float jumpTime;
	private float jumpTimeCounter = 0f;
	private bool stoppedJumping = true;
	
	private void Awake()
	{
		PlayerTouchHandler.OnPlayerJump += Jump;
		
		GetPlayerComponents();
		
		jumpTimeCounter = jumpTime;
	}
	private void Update()
	{
		isGrounded = Physics.Raycast(transform.position,Vector3.down,2 * .5f + .2f, iAmGround);
		
		if(isGrounded)
		{
			jumpTimeCounter = jumpTime;
		}
	}
	
	private void GetPlayerComponents()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Jump()
	{
		
	}
}
