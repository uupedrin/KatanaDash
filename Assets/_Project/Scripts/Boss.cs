using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    int health;
    int heat = 0;
    [SerializeField] float cooldown;
    float nextAttack;
    [SerializeField] float attack1Cooldown;

    [Header("References")]
    public Player player;
    [SerializeField] GameObject[] projectile;

    void OnEnable()
    {
        health = maxHealth;
        transform.position = new Vector3(player.gameObject.transform.position.x + 60, 0, 0);
        nextAttack = Time.time + 3;
    }

    void Update()
    {
        transform.position += player.moveSpeed * Time.deltaTime * Vector3.right;
        /*if(Time.time >= nextAttack)
        {
            switch(Random.Range(0,1))
            {
                case 0:
                Attack1();
                break;
            }
        }*/
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision == player.gameObject)
        {
            if(heat < 3) heat++;
            else 
            {
                health--;
                heat = 0;
                if(health <= 0)
                {
                    health = maxHealth;
                    GameManager.manager.bossFight = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void Attack1()
    {
        Attack1_1();
        Invoke("Attack1_2", attack1Cooldown);
        Invoke("Attack1_1", attack1Cooldown * 2);
    }

    void Attack1_1()
    {
        for(int i = 0; i < 3; i++)
        {
            projectile[i].SetActive(true);
            projectile[i].transform.position = new Vector3(0, 0.3f, 1.7f);
        }
        nextAttack = Time.time + cooldown;
    }
    
    void Attack1_2()
    {
        for(int i = 3; i < 5; i++)
        {
            projectile[i].SetActive(true);
            projectile[i].transform.position = new Vector3(0, 0.3f, 1.7f);
        }
    }
}
