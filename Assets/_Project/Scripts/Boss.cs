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
    float nextAttack;
    int heat;
    bool waiting;
    bool attacking;
    bool retreating;

    void OnEnable()
    {
        heat = 0;
        waiting = true;
        health = maxHealth;
        transform.position = new UnityEngine.Vector3(player.transform.position.x - (player.transform.position.x % level.blockSize) + level.blockSize * 2, 0, 0);
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
            if(transform.position.x <= player.transform.position.x - 20 || transform.position.x > player.transform.position.x + 10)
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
            if(transform.position.y <= 0.3f && transform.position.y >= -0.3f) transform.position = new UnityEngine.Vector3(transform.position.x, 0, 0);
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
                switch(Random.Range(0,1))
                {
                    case 0:
                    Attack1();
                    break;
                }
            }
            transform.position = new UnityEngine.Vector3(player.transform.position.x + bossDistance, transform.position.y, 0);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            if(!player.isDashing) player.Die();
            else
            {
                attacking = false;
                retreating = true;
                health--;
                if(health <= 0)
                {
                    health = maxHealth;
                    gameObject.SetActive(false);
                    GameManager.manager.bossFight = false;
                }
            }
        }
    }

    void Attack1()
    {
        nextAttack = Time.time + cooldown;
        heat++;
    }

    void PrepareCharge()
    {
        heat = 0;
        Invoke("StartCharge", chargeDelay);
    }

    void StartCharge()
    {
        attacking = true;
    }
}
