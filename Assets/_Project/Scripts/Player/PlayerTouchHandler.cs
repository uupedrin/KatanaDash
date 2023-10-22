using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
	
	// Actions
	public static Action<bool> OnPlayerJump;
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
			bool touchLeft = firstTouch.position.x < halfScreenSize;
			
			if(firstTouch.phase == TouchPhase.Began)
			{
				if(touchLeft)
				{
					OnPlayerJump?.Invoke(true);
				}
				else
				{
					OnPlayerDash?.Invoke();
				}
			}
			
			if(firstTouch.phase == TouchPhase.Ended && touchLeft)
			{
				OnPlayerJump?.Invoke(false);
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
