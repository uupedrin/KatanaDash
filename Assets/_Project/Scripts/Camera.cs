using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float duration;
    [SerializeField]
    float magnitude;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    public IEnumerator Shake()
    {
        transform.position += new Vector3(0, 0, magnitude);
        transform.position -= new Vector3(0, 0, 2 * magnitude);
        yield return new WaitForSeconds(duration);
    }
}
