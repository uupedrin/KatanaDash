using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int health;
    [SerializeField] Player player;
    int heat = 0;
    [SerializeField] GameObject projectile;
    bool waiting;
    [SerializeField] float cooldown;
    float nextAttack;
    [SerializeField] float attack1CD;
    [SerializeField] GameObject gun;
    [SerializeField] float acceleration;

    void OnEnable()
    {
        waiting = true;
        health = maxHealth;
        nextAttack = Time.time + cooldown;
        transform.position = new Vector3(player.transform.position.x - (player.transform.position.x % 40) + 140, 0, 0);
    }

    void Update()
    {
        if(waiting & transform.position.x - player.transform.position.x <= 10) waiting = false;
        else if(!waiting) 
        {
            if(heat < 1) transform.position += player.moveSpeed * Time.deltaTime * Vector3.right;
            else
            {
                StartCoroutine(FindPlayer());
            }
            if(nextAttack <= Time.time)
            {
                switch(Random.Range(0,1))
                {
                    case 0:
                    Attack1();
                    break;

                    case 1:
                    Attack2();
                    break;
                }
                heat++;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {   
        if(collision == player.gameObject)
        {
            if(player.isDashing)
            {
                health--;
                heat = 0;
                if(health <= 0)
                {
                    health = maxHealth;
                    gameObject.SetActive(false);
                }
                StartCoroutine(Flinch());
            }
            else player.Die();
        }
    }

    void Attack1()
    {
        Attack1_1();
        Invoke("Attack1_2", attack1CD);
        Invoke("Attack1_1", attack1CD * 2);
    }

    void Attack1_1()
    {
        GameObject projectile1 = Instantiate(projectile, transform.position, transform.rotation);
        GameObject projectile2 = Instantiate(projectile, transform.position, transform.rotation);
        GameObject projectile3 = Instantiate(projectile, transform.position, transform.rotation);
        projectile1.transform.Rotate(0, 90, 60);
        projectile2.transform.Rotate(0, 90, -60);
        projectile3.transform.Rotate(0, 90, 0);
        nextAttack = Time.time + cooldown;
    }

    void Attack1_2()
    {
        GameObject projectile1 = Instantiate(projectile, transform.position, transform.rotation);
        GameObject projectile2 = Instantiate(projectile, transform.position, transform.rotation);
        projectile1.transform.Rotate(0, 90, 30);
        projectile2.transform.Rotate(0, 90, -30);
    }

    void Attack2()
    {
        gun.SetActive(true);
        nextAttack = Time.time + cooldown;
    }

    IEnumerator FindPlayer()
	{
		transform.position += player.moveSpeed * Time.deltaTime * Vector3.right;
        if(transform.position.y >= player.transform.position.y) transform.position -= new Vector3(0, 0.1f, 0);
        else transform.position += new Vector3(0, 0.1f, 0);
		yield return new WaitUntil(() => transform.position.y <= player.transform.position.y + 0.5f && transform.position.y >= player.transform.position.y - 0.5f);
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        StartCoroutine(Charge());
        StopCoroutine(FindPlayer());
	}

    IEnumerator Charge()
	{
		yield return new WaitUntil(() => transform.position.x <= player.transform.position.x);
	}

    IEnumerator Flinch()
	{
		transform.position = new Vector3(player.transform.position.x + 10, transform.position.y, 0);
        transform.position += 0.5f * Time.deltaTime * Vector3.right;
		yield return new WaitUntil(() => transform.position.x <= player.transform.position.x - 100);
        transform.position = new Vector3(player.transform.position.x + 10, 0, 0);
	}
}