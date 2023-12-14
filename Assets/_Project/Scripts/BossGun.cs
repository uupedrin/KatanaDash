using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    [SerializeField] Player player;
    bool waiting;
    [SerializeField] GameObject beam;
    [SerializeField] float delay;
    bool beamGrow;
    bool goAway;

    void OnEnable()
    {
        waiting = true;
        beamGrow = false;
        goAway = false;
        transform.position = new UnityEngine.Vector3(player.transform.position.x + 50, -4, 0);
    }

    void Update()
    {
        if(waiting & transform.position.x - player.transform.position.x <= 10) 
        {
            waiting = false;
            Invoke("Shoot", delay);
        }
        else if(!waiting) 
        {
            transform.position = new UnityEngine.Vector3(player.transform.position.x + 10, -4, 0);
            if(beamGrow) 
            {
                beam.transform.localScale += new UnityEngine.Vector3(0.1f, 0, 0.1f);
                if(beam.transform.localScale.x >= 2) beam.SetActive(false);
                goAway = true;
            }
            if(goAway) 
            {
                transform.position += 5 * Time.deltaTime * UnityEngine.Vector3.right;
                Invoke("Leave", 3);
            }
        }
    }

    void Shoot()
    {
        beam.SetActive(true);
        beam.transform.position = new UnityEngine.Vector3(transform.position.x - 50, transform.position.y, transform.position.z);
        beamGrow = true;
    }

    void Leave()
    {
        gameObject.SetActive(false);
    }
}
