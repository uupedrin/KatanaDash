using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
	[SerializeField] int maxHealth;
	int health;
	[SerializeField] Player player;
	[SerializeField] LevelManager level;
	[SerializeField] float bossDistance;
	[SerializeField] float cooldown;
	[SerializeField] float speedCharge;
	[SerializeField] float chargeDelay;
	[SerializeField] float chargePrepareTime;
	[SerializeField] float respawnDistance;
	Animator animator;
	float nextAttack;
	int heat;
	bool waiting;
	bool attacking;
	bool retreating;
	bool canGo;
	bool isDying;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
	}
	
	void OnEnable()
	{
		isDying = false;
		canGo = false;
		heat = 0;
		waiting = true;
		attacking = false;
		retreating = false;
		health = maxHealth;
		transform.position = new UnityEngine.Vector3(player.transform.position.x - (player.transform.position.x % level.blockSize) + level.blockSize * 2, 2, 0);
	}

	void Update()
	{
		if(waiting && transform.position.x <= player.transform.position.x + bossDistance) 
		{
			waiting = false;
			nextAttack = Time.time + cooldown;
		}
		if(attacking) 
		{
			transform.position += speedCharge * Time.deltaTime * UnityEngine.Vector3.left;
			if(transform.position.x <= player.transform.position.x - 20 || transform.position.x > player.transform.position.x + bossDistance)
			{
				attacking = false;
				waiting = true;
				transform.position = new UnityEngine.Vector3(player.transform.position.x + 30, 0, 0);
			}
		}
		else if(retreating)
		{
			transform.position += 70 * Time.deltaTime * UnityEngine.Vector3.right;
			if(transform.position.y <= 0) transform.position += 20 * UnityEngine.Vector3.up * Time.deltaTime;
			else transform.position += 20 * UnityEngine.Vector3.down * Time.deltaTime;
			if(transform.position.y <= 0.3f && transform.position.y >= -0.3f) transform.position = new UnityEngine.Vector3(transform.position.x, 2, 0);
			if(transform.position.x >= player.transform.position.x + bossDistance) retreating = false;
		}
		else if(!waiting) 
		{
			if(heat > 2)
			{
				if(transform.position.y <= player.transform.position.y + 0.3f && transform.position.y >= player.transform.position.y - 0.3f) 
				{
					transform.position = new UnityEngine.Vector3(transform.position.x, player.transform.position.y, 0);
					Invoke("PrepareCharge", chargePrepareTime);
				}
				else if(transform.position.y <= player.transform.position.y) transform.position += 10 * UnityEngine.Vector3.up * Time.deltaTime;
				else transform.position += 10 * UnityEngine.Vector3.down * Time.deltaTime;
			}
			else if(heat <= 2 && nextAttack <= Time.time)
			{
				canGo = true;
				switch(Random.Range(0,1))
				{
					case 0:
					SetUp();
					break;
				}
			}
			if(!isDying) transform.position = new UnityEngine.Vector3(player.transform.position.x + bossDistance, transform.position.y, 0);
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if(collision.tag == "Player")
		{
			if(!player.isDashing && !player.isStabbing  && !GameManager.manager.immortal) player.Die();
			else
			{
				TakeDamage();
			}
		}
		else if(collision.tag == "PlayerProjectile") TakeDamage();
	}

	void SetUp()
	{
		nextAttack = Time.time + cooldown;
		heat++;
	}

	void PrepareCharge()
	{
		heat = 0;
		animator.SetTrigger("StartAttack");
		GetComponent<Collider>().enabled = true;
		Invoke("StartCharge", chargeDelay);
	}

	void StartCharge()
	{
		if(canGo) attacking = true;
		canGo = false;
	}

	void RespawnBoss()
	{
		transform.position = player.transform.position + respawnDistance * UnityEngine.Vector3.right;
		respawnDistance -= Mathf.Clamp( respawnDistance * 0.32f, 40, 240);
		isDying = false;
	}

	void TakeDamage()
	{
		GetComponent<Collider>().enabled = false;
		animator.SetTrigger("TakeDamage");
		attacking = false;
		retreating = true;
		if(!GameManager.manager.damageCheat) health--;
		else health = 0;
		if(health <= 0)
		{
			health = 1;
			GameManager.manager.SetAchievement(5);
			GameManager.manager.UiManager.PopUp(5);
			GameManager.manager.bossFight = false;
			isDying = true;
			retreating = false;
			animator.SetTrigger("Die");
			Invoke("RespawnBoss", 1.75f);
		}
	}
}
