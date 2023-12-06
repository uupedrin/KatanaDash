using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    int health;
    Player player;
    int heat = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += player.moveSpeed * Time.deltaTime * Vector3.right;
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
                    gameObject.SetActive(false);
                }
            }
        }

    }
}
