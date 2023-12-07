using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    int health;
    [SerializeField]
    Player player;
    int heat = 0;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float shootspeed;
    [SerializeField]
    float fastshootspeed;
    [SerializeField]
    float cooldown;
    float nextAttack;
    int attackNumber =-1;
    [SerializeField]
    float cooldownAttack1;

    void OnEnable()
    {
        health = maxHealth;
        nextAttack = Time.time + cooldown;
    }

    void Update()
    {
        if(heat>=3) transform.position += player.moveSpeedStart * Time.deltaTime * Vector3.right;
        else transform.position += player.moveSpeed * Time.deltaTime * Vector3.right;
        if(Time.time >= nextAttack) 
        {
            attackNumber = Random.Range(0,3);
            switch(attackNumber)
            {
                case 0:
                Attack1();
                break;
            }
            nextAttack = Time.time + cooldown;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision == player.gameObject)
        {
            health--;
            if(health<=0) gameObject.SetActive(false);
        }
    }

    void Attack1()
    {
        Attack1_1();
        Invoke("Attack1_2", cooldownAttack1);
        Invoke("Attack1_1", cooldownAttack1 * 2);
    }

    void Attack1_1()
    {
        GameObject projectile1 = Instantiate(projectile, transform.position, transform.rotation);
        projectile1.transform.Rotate(30, 0, 0);
        GameObject projectile2 = Instantiate(projectile, transform.position, transform.rotation);
        projectile2.transform.Rotate(-30, 0, 0);
        GameObject projectile3 = Instantiate(projectile, transform.position, transform.rotation);
        projectile1.GetComponent<Rigidbody>().AddForce(Vector3.left * shootspeed * Time.deltaTime);
        projectile2.GetComponent<Rigidbody>().AddForce(Vector3.left * shootspeed * Time.deltaTime);
        projectile3.GetComponent<Rigidbody>().AddForce(Vector3.left * shootspeed * Time.deltaTime);

    }

    void Attack1_2()
    {
        GameObject projectile1 = Instantiate(projectile, transform.position, transform.rotation);
        projectile1.transform.Rotate(60, 0, 0);
        GameObject projectile2 = Instantiate(projectile, transform.position, transform.rotation);
        projectile2.transform.Rotate(-60, 0, 0);
        projectile1.GetComponent<Rigidbody>().AddForce(Vector3.left * shootspeed * Time.deltaTime);
        projectile2.GetComponent<Rigidbody>().AddForce(Vector3.left * shootspeed * Time.deltaTime);
    }
}
