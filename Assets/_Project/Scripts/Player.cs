using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Ground Check")]
	[SerializeField]
	private LayerMask iAmGround;
	private bool isGrounded;
	
	private void Update()
	{
		isGrounded = Physics.Raycast(transform.position,Vector3.down,2 * .5f + .2f, iAmGround);
	}
}
