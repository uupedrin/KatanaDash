using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] float moveSpeedStart;
	public float moveSpeed;

	[Header("Dash")]
	[SerializeField] float dashSpeed;
	[SerializeField] float dashDuration;
	[SerializeField] float dashCooldown;
	float nextDash = 0;
	bool isDashing;
	bool canDash;

	[Header("Jump")]
	[SerializeField] float jumpForce;
	[SerializeField] float airJumpForce;
	[SerializeField] float jumpHold;
	[SerializeField] float dashJumpForce;
	bool leftGround = false;
	bool canJump;
	bool jumpButtonHeld = false;
	Rigidbody body;
	private float halfScreen;
	
	[Header("References")]
	[SerializeField] LayerMask layer;
	[SerializeField] Camera cameraShake;
	[SerializeField] LevelManager procedural;
	[SerializeField] GameObject projectile;
	[SerializeField] GameObject bossTrigger;

	[Header("PowerUp")]
	bool isStabbing = false;
	bool hasDashShoot = false;
	[SerializeField] float dashPowerUpDuration;
	
	[Header("Other")]
	[SerializeField] float cheatSpeed;
	[SerializeField] Animator playerAnimator;
	void Start()
	{
		playerAnimator = GetComponentInChildren<Animator>();
		body = GetComponent<Rigidbody>();
		halfScreen = Screen.width / 2f;
		moveSpeed = moveSpeedStart;
	}

	void Update()
	{
		Debug.Log(leftGround);
		if(body.velocity.y <= -.5)
		{
			playerAnimator.SetBool("isFalling", true);
		}
		else if(IsGrounded())
		{
			if(!leftGround)	playerAnimator.SetBool("isFalling", false);
		}
	}
	
	void FixedUpdate()
	{
		if(GameManager.manager.UiManager.isPaused) return;
		
		transform.position += UnityEngine.Vector3.right * Time.deltaTime * moveSpeed;
		if(IsGrounded()) 
		{
			canDash = true;
			canJump = true;
		}
		
		if(jumpButtonHeld)
		{
			if(canJump) 
			{
				Jump();
				Invoke("CantJump", jumpHold);
			}
		}
		if(Input.touchCount >=4) StartCoroutine(Cheat());
		if(body.velocity.y <= -.5)
		{
			body.AddForce(UnityEngine.Vector3.up * -1 * 20);
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		switch(collision.gameObject.tag)
		{
			case "Enemy":
			if(isDashing || isStabbing) 
			{
				Instantiate(AudioManager.manager.katanaHit, transform.position, transform.rotation);
				Instantiate(AudioManager.manager.explosion, transform.position, transform.rotation);
				Kill(collision);
				if(!isDashing) isStabbing = false;
			}
			else 
			{
                Instantiate(AudioManager.manager.damage, transform.position, transform.rotation);
				Die();
            }
			break;

			case "NotDashableEnemy":
            Instantiate(AudioManager.manager.damage, transform.position, transform.rotation);
            Die();
			break;

			case "TutorialEnemy":
			if(isDashing || isStabbing) 
			{
				Kill(collision);
                Instantiate(AudioManager.manager.katanaHit, transform.position, transform.rotation);
                if (!isDashing) isStabbing = false;
			}
			else
			{
				CantJump();
				Recoil();
			}
			break;

			case "TutorialNotDashableEnemy":
			CantJump();
			Recoil();
			break;

			case "DashJump":
			if(isDashing) 
			{
				StopAllCoroutines();
				StopDash();
				Instantiate(AudioManager.manager.jumpPwP, transform.position, transform.rotation);
				body.AddForce(UnityEngine.Vector3.up * dashJumpForce);
			}
			break;

			case "Coin":
			Instantiate(AudioManager.manager.coin, transform.position, transform.rotation);
			GameManager.manager.AddPoints(1);
			collision.gameObject.SetActive(false);
			GameManager.manager.SetAchievement(0);
			break;

			case "TutorialHole":
			Recoil();
			body.AddForce(UnityEngine.Vector3.up * 500);
			break;

			case "BlockCaller":
			procedural.Rearrange();
			break;

			case "DashPowerUp":
			hasDashShoot = true;
			Invoke("DashPowerUpOver", dashPowerUpDuration);
			collision.gameObject.SetActive(false);
			break;

			case "StabPowerUp":
			isStabbing = true;
			collision.gameObject.SetActive(false);
			break;
		}
	}
	
	//CONTROL HANDLING FUNCTIONS
	public void JumpButtonPress(bool state)
	{
		if(GameManager.manager.UiManager.isPaused) return;
		
		jumpButtonHeld = state;
	}
	public void DashButtonPress()
	{
		if(GameManager.manager.UiManager.isPaused) return;
		
		if(!isDashing)
		{
			if(canDash && Time.time >= nextDash)
			{
				StartCoroutine(Dash());
			}
		}
	}
	
	//RECURRING METHODS__________________________________________________________________
	
	void Jump()
	{
		if(IsGrounded())
		{
			body.AddForce(UnityEngine.Vector3.up * jumpForce);
			playerAnimator.SetTrigger("StartJumping");
			leftGround = true;
			Invoke("leftGroundFalse", .3f);
		}
		else if(canJump) body.AddForce(UnityEngine.Vector3.up * airJumpForce);
	}

	IEnumerator Dash()
	{
		playerAnimator.SetTrigger("PlayerDash");
		if(hasDashShoot) projectile.SetActive(true);
		canDash = false;
		isDashing = true;
		Instantiate(AudioManager.manager.dash, transform.position, transform.rotation);
		body.constraints = RigidbodyConstraints.FreezeAll;
		moveSpeed = dashSpeed;
		yield return new WaitForSeconds(dashDuration);
		StopDash();
	}

	void StopDash()
	{
		moveSpeed = moveSpeedStart;
		body.constraints = RigidbodyConstraints.FreezeRotation;
		isDashing = false;
		nextDash = Time.time + dashCooldown;
	}
	public void Kill(Collider collision)
	{
		playerAnimator.SetTrigger("PlayerHit");
		StartCoroutine(GameManager.manager.Freeze());
		GameManager.manager.AddPoints(2);
		collision.gameObject.SetActive(false);
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
	
	void Die()
	{
		GameManager.manager.EndGame();
		GameManager.manager.UiManager.EndGameScreen(GameManager.manager.finalMeters, GameManager.manager.coins);
		GameManager.manager.HighScore();
	}

	bool IsGrounded()
	{
		return Physics.Raycast(gameObject.transform.position, UnityEngine.Vector3.down, 1.025874f, layer);
	}

	IEnumerator Cheat()
	{
		body.detectCollisions = false;
		body.constraints = RigidbodyConstraints.FreezeAll;
		moveSpeed = cheatSpeed;
		yield return new WaitUntil(() => transform.position.x >= bossTrigger.transform.position.x - 5);
		CheatStop();
	}

	void CheatStop()
	{
		moveSpeed = moveSpeedStart;
		body.constraints = RigidbodyConstraints.FreezeRotation;
		body.detectCollisions = true;
	}
	
	//INVOKABLE METHODS__________________________________________________________________

	void CantJump()
	{
		canJump = false;
	}

	void DashPowerUpOver()
	{
		hasDashShoot = false;
	}

	void NormalMoveSpeed()
	{
		moveSpeed = moveSpeedStart;
		isDashing = false;
	}

	void leftGroundFalse()
	{
		leftGround = false;
		playerAnimator.SetBool("isFalling", true);
	}
}