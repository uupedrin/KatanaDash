using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

//Requires PlayerTouchHandler
[RequireComponent(typeof(PlayerTouchHandler))]
public class Player : MonoBehaviour
{
	[SerializeField]
	float moveSpeedStart;
	float moveSpeed;
	[SerializeField]
	float dashSpeed;
	[SerializeField]
	float dashDuration;
	[SerializeField]
	float jumpForce;
	[SerializeField]
	float airJumpForce;
	[SerializeField]
	float dashJumpForce;
	[SerializeField]
	float jumpHold;
	Rigidbody body;
	private float halfScreen;
	bool isDashing;
	bool canJump;
	bool canDash;
	[SerializeField]
	float dashCooldown;
	float nextDash = 0;
	public LayerMask layer;
	
	[SerializeField]
	Camera cameraShake;
	[SerializeField]
	LevelManager procedural;

	void Start()
	{
		body = GetComponent<Rigidbody>();
		halfScreen = Screen.width / 2f;
		moveSpeed = moveSpeedStart;
	}

	void FixedUpdate()
	{
		transform.position += UnityEngine.Vector3.right * Time.deltaTime * moveSpeed;
		if(IsGrounded()) 
		{
			canDash = true;
			canJump = true;
		}
		if(Input.touchCount > 0)
		{
			Touch playerTouch = Input.GetTouch(0);
			if(playerTouch.position.x > halfScreen && !isDashing)
			{
				if(canDash && Time.time >= nextDash)
				{
					StartCoroutine(Dash());
				}
			}
			else if (playerTouch.position.x <= halfScreen)
			{
				if(canJump) 
				{
					Jump();
					Invoke("CantJump", jumpHold);
				}
			}
		}
		if(body.velocity.y <= -1)
		{
			//body.AddForce(Vector3.U)
		}
	}

	void Jump()
	{
		if(IsGrounded()) body.AddForce(UnityEngine.Vector3.up * jumpForce);
		else if(canJump) body.AddForce(UnityEngine.Vector3.up * airJumpForce);
	}

	IEnumerator Dash()
	{
		canDash = false;
		isDashing = true;
		//body.useGravity = false;
		body.constraints = RigidbodyConstraints.FreezeAll;
		moveSpeed = dashSpeed;
		yield return new WaitForSeconds(dashDuration);
		StopDash();
	}

	void StopDash()
	{
		moveSpeed = moveSpeedStart;
		body.constraints = RigidbodyConstraints.FreezeRotation;
		//body.useGravity = true;
		isDashing = false;
		nextDash = Time.time + dashCooldown;
	}

	void OnTriggerEnter(Collider collision)
	{
		switch(collision.gameObject.tag)
		{
			case "Enemy":
			if(!isDashing) Die();
			else
			{
				Kill(collision);
			}
			break;

			case "NotDashableEnemy":
			Die();
			break;

			case "TutorialEnemy":
			if(!isDashing)
			{
				CantJump();
				Recoil();
			}
			else
			{
				Kill(collision);
			}
			break;

			case "TutorialNotDashableEnemy":
			CantJump();
			Recoil();
			break;

			case "DashPowerUp":
			if(isDashing) 
			{
				StopAllCoroutines();
				StopDash();
				body.AddForce(UnityEngine.Vector3.up * dashJumpForce);
			}
			break;

			case "Coin":
			GameManager.manager.AddPoints(1);
			collision.gameObject.SetActive(false);
			break;

			case "TutorialHole":
			Recoil();
			body.AddForce(UnityEngine.Vector3.up * 500);
			break;

			case "BlockCaller":
			procedural.Rearrange();
			break;
		}
	}

	void Die()
	{
		GameManager.manager.UiManager.ChangeScene("Game");
	}

	bool IsGrounded()
	{
		return Physics.Raycast(gameObject.transform.position, UnityEngine.Vector3.down, 1.025874f, layer);
	}

	void CantJump()
	{
		canJump = false;
	}

	void NormalMoveSpeed()
	{
		moveSpeed = moveSpeedStart;
		isDashing = false;
	}

	void Recoil()
	{
		isDashing = true;
		body.constraints = RigidbodyConstraints.FreezeAll;
		body.constraints = RigidbodyConstraints.FreezeRotation;
		body.AddForce(UnityEngine.Vector3.left * 200);
		moveSpeed = 0;
		Invoke("NormalMoveSpeed", 2f);
	}

	void Kill(Collider collision)
	{
		StartCoroutine(cameraShake.Shake());
		StartCoroutine(GameManager.manager.Freeze());
		GameManager.manager.AddPoints(2);
		collision.gameObject.SetActive(false);
	}
}