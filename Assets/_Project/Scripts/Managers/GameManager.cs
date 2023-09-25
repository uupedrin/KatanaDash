using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public int score = 0;
	public static GameManager manager;
	public UIManager UiManager;
	void Awake()
	{
		if (manager == null)
		{
			manager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(gameObject);
		
		SceneManager.activeSceneChanged += ResetGame;
	}

	public void AddPoints(int enemyType)
	{
		switch (enemyType)
		{
			case 1:
				score += 500;
				break;
			case 2:
				score += 1000;
				break;
			case 3:
				score += 1500;
				break;
		}
		UiManager.SetStatus(score);
	}
	
	public void ResetGame(Scene current, Scene next)
	{
		if(next.name == "Game")
		{
			score = 0;
		}
	}
}
