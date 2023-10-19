using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

//Requires PlayerTouchHandler
[RequireComponent(typeof(PlayerTouchHandler))]

public class PlayerTest : MonoBehaviour
{

	Rigidbody body;
    //gravity
	bool playerGrounded;
	bool useGravity = true;

	[Header("Ground Check")]
	[SerializeField]
	private LayerMask iAmGround;
	private bool isGrounded;

	//movement
	[Header("Movement")]
	[SerializeField]
	float initialspeed;
	float movespeed;

    //jumping
	[Header("Jumping")]
	[SerializeField]
	float maxJumpHeight = 4.0f;
	[SerializeField]
	float maxJumpTime = .75f;
	bool isJumpPressed = false;
	[SerializeField]
	float initialJumpVelocity;
	[SerializeField]
	float airJumpVelocity;
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
	
    void Start()
	{
		SubscribeToEvents();
		movespeed = initialspeed;
		body = GetComponent<Rigidbody>();
		//SetupJumpVariables();
	}

    void Update()
	{
		isGrounded = Physics.Raycast(transform.position,UnityEngine.Vector3.down, 2 * .5f + .2f);
		transform.position += UnityEngine.Vector3.right * Time.deltaTime * movespeed;
		Console.WriteLine(playerGrounded);
		HandleJump();
		Debug.Log(body.velocity);
	}


    void SubscribeToEvents()
	{
		PlayerTouchHandler.OnPlayerJump += OnJump;
		PlayerTouchHandler.OnPlayerDash += OnDash;
	}

    void OnJump(bool value)
	{
		isJumpPressed = value;
	}

	void HandleJump()
	{
		if(isJumpPressed && isGrounded) //Change to can jump
		{
			isJumping = true;
			body.AddForce(transform.up * initialJumpVelocity);
			
		}
		else if (isJumpPressed && isJumping &&body.velocity.y <=10) body.AddForce(transform.up * airJumpVelocity);
		else
		{
			isJumping = false;
		}
	}
    
    /*void SetupJumpVariables()
	{
		float timeToApex = maxJumpTime / 2;
		initialJumpVelocity = 2 * maxJumpHeight / timeToApex;
	}*/
	
	void OnDash()
	{
		if(canDash && DashCoroutine == null)
		{
			DashCoroutine = StartCoroutine(Dash());
		}
	}
	IEnumerator Dash()
	{
		isDashing = true;
		body.constraints = RigidbodyConstraints.FreezeAll; //During dash, can't be moved or rotated. NOTE: If only FreezePositionY is used, during the transition to FreezeRotation, both are disabled (???) but FreezeAll works as intended
		movespeed = dashSpeed;
		yield return new WaitForSeconds(dashDuration);
		movespeed = initialspeed;
		isDashing = false;
		body.constraints =  RigidbodyConstraints.FreezeRotation;
		//DashCoroutine = null;
	}
	public bool IsDashing
	{
		get{return isDashing;}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(!isDashing && collision.gameObject.tag == "Enemy") 
		{
			//add feedback
			//resetar fase
			Die();
		}
		else if(isDashing && collision.gameObject.tag == "Enemy") 
		{
			Destroy(collision.gameObject);
		}
		else if(collision.gameObject.tag == "Coin") 
		{
			GameManager.manager.AddPoints(1); 
			Destroy(collision.gameObject);
			//add feedback
		}
		else if (collision.gameObject.tag == "NotDashableEnemy") 
		{
			//add feedback
			//resetar fase
			Die();
		}
		else if (collision.gameObject.tag == "End") 
		{
			//tela de vitoria
			GameManager.manager.UiManager.ChangeScene("Victory");
		}
	}

	void Die()
	{
		//StopAllCoroutines();
		//DashCoroutine = null;
		GameManager.manager.UiManager.ChangeScene("Defeat");
	}
}
