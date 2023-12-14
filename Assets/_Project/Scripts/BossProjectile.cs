using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += speed * transform.right * -1 * Time.deltaTime;
    }
}
