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
	float jumpHold;
	Rigidbody body;
	private float halfScreen;
	[SerializeField]
	float maxVelocity;
	bool isDashing;
	bool canJump;
	bool canDash;
	[SerializeField]
	float dashCooldown;
	float nextDash = 0;

	void Start()
	{
		body = GetComponent<Rigidbody>();
		halfScreen = Screen.width / 2f;
		moveSpeed = moveSpeedStart;
	}

	void FixedUpdate()
	{
		Debug.Log(canJump);
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
		moveSpeed = moveSpeedStart;
		body.constraints = RigidbodyConstraints.FreezeRotation;
		//body.useGravity = true;
		isDashing = false;
		nextDash = Time.time + dashCooldown;
	}

	void OnTriggerEnter(Collider collision)
	{
		if((collision.gameObject.tag == "Enemy" && !isDashing) || collision.gameObject.tag == "NotDashableEnemy")
		{
			Die();
		}
		else if(collision.gameObject.tag == "Enemy" && isDashing)
		{
			canJump = true;
			GameManager.manager.AddPoints(2);
			Destroy(collision.gameObject);
		}
		else if(collision.gameObject.tag == "Coin")
		{
			GameManager.manager.AddPoints(1);
			Destroy(collision.gameObject);
		}
		else if(collision.gameObject.tag == "TutorialHole")
		{
			Recoil();
			body.AddForce(UnityEngine.Vector3.up * 500);
		}
		else if(collision.gameObject.tag == "TutorialEnemy" || (collision.gameObject.tag == "TutorialEnemyDash" && !isDashing))
		{
			CantJump();
			Recoil();
		}
	}

	void Die()
	{
		GameManager.manager.UiManager.ChangeScene("Game");
	}

	bool IsGrounded()
	{
		return Physics.Raycast(gameObject.transform.position, UnityEngine.Vector3.down, 1.025874f);
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
}