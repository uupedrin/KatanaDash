using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchHandler : MonoBehaviour
{
	
	// Actions
	public static Action<bool> OnPlayerJump;
	public static Action OnPlayerDash;
	
	public void JumpButton(bool state)
	{
		OnPlayerJump?.Invoke(state);
	}
	public void DashButton()
	{
		OnPlayerDash?.Invoke();
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
