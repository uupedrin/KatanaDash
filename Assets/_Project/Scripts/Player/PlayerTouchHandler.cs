using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
	
	public static Action OnPlayerJump;	
	public static Action OnPlayerStopJump;
	public static Action OnPlayerDash;
	
	private float halfScreenSize;
	
	void Awake()
	{
		halfScreenSize = Screen.width / 2f;
	}

	void Update()
	{
		bool playerTouch = Input.touchCount > 0;
		if(playerTouch)
		{
			
			Touch firstTouch = Input.GetTouch(0);
			if(firstTouch.phase == TouchPhase.Began)
			{
				if(firstTouch.position.x < halfScreenSize)
				{
					Debug.Log("Player touch: LEFT");
					OnPlayerJump?.Invoke();
				}
				else
				{
					Debug.Log("Player touch: RIGHT");
					OnPlayerDash?.Invoke();
				}
			}
			
			if(firstTouch.phase == TouchPhase.Ended && firstTouch.position.x < halfScreenSize)
			{
				Debug.Log("Parou de pular");
				OnPlayerStopJump?.Invoke();
			}
			
		}
	}
	
	private void HandleCheats()
	{
		bool nextLevel = Input.touchCount == 4;
		bool infiniteHealth = Input.touchCount == 5;
		
		if(nextLevel)
		{
			//Call next level
		}
		
		if(infiniteHealth)
		{
			//Toggle infinite health
		}
	}
}