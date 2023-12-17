using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float force;
    [SerializeField]
    float duration;
    [SerializeField]
    Player player;
    [SerializeField]
    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        transform.position = player.transform.position + new UnityEngine.Vector3(1, 0, 0);
        Invoke("Disable", duration);
        body.constraints = RigidbodyConstraints.FreezeAll;
        body.constraints = RigidbodyConstraints.FreezeRotation;
        body.AddForce(UnityEngine.Vector3.right * force);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy") player.Kill(collision); 
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}