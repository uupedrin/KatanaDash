using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectiles : MonoBehaviour
{
    [SerializeField] float speed;

    void OnEnable()
    {
        Invoke("End", 1);
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime * -transform.right;
    }

    void End()
    {
        gameObject.SetActive(false);
    }
}
