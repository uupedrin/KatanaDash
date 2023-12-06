using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public int score = 0;
	public static GameManager manager;
	public UIManager UiManager;
	public float freezeDuration;
	[SerializeField]
	Achievements data;
	void Awake()
	{
		data = new Achievements();
		HighScore();
		if (manager == null)
		{
			manager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		SaveToJson();
		LoadFromJson();
		DontDestroyOnLoad(gameObject);
		
		SceneManager.activeSceneChanged += ResetGame;
	}

	public void AddPoints(int enemyType)
	{
		switch (enemyType)
		{
			case 1:
				score += 500;
				data.achievement[0] = true;
				data.records[0]++;
				SaveToJson();
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

	public IEnumerator Freeze()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(freezeDuration);
		Time.timeScale = 1;
	}

	public void HighScore()
	{
		if(!File.Exists("highscore.json") || Convert.ToInt32(File.ReadAllText("highscore.json")) < score) File.WriteAllText("highscore.json", score.ToString());
	}

	public void NewAchievement(int achievementID)
	{
		data.achievement[achievementID] = true;
    }

	public void SaveToJson()
	{
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(Application.persistentDataPath + "/AchievementsDataFile.json", json);
	}
	public void LoadFromJson()
	{
		string json = "";
		if(File.Exists(Application.persistentDataPath + "/AchievementsDataFile.json"))
		{
			json = File.ReadAllText(Application.persistentDataPath + "/AchievementsDataFile.json");
		}
		data = JsonUtility.FromJson<Achievements>(json);
	}
}

/*
ACHIEVEMENTS:
0 - First Dash
1 - Collected 10 coins in one run
2 - Collected 30 total coins
3 - Died while in the air
*/