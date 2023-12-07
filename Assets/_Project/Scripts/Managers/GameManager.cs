using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	public int coins = 0;
	[SerializeField]
	public float metersRan;
	[SerializeField]
	public float metersTimer;
	[SerializeField]
	public float finalMeters;
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
	}
	private void Update()
	{
		metersTimer += Time.deltaTime;
		metersRan = 2.08f * metersTimer;
		metersRan = (float)Math.Round(metersRan,1);
		if(finalMeters == 0)
		{
			UiManager.SetStatus(metersRan);
		}
	}
	public void AddPoints(int enemyType)
	{
		switch (enemyType)
		{
			case 1: // coins
				coins += 1;
				data.achievement[0] = true;
				data.records[0]++;
				SaveToJson();
				UiManager.SetCoins(coins);
				break;
			case 2:
				//score += 1000;
				break;
			case 3:
				//score += 1500;
				break;
		}
	}
	public void EndGame()
	{
		finalMeters = metersRan;
	}
	public IEnumerator Freeze()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(freezeDuration);
		Time.timeScale = 1;
	}
	public void HighScore()
	{
		if(!File.Exists("highscore.json") || Convert.ToInt32(File.ReadAllText("highscore.json")) < metersRan) File.WriteAllText("highscore.json", metersRan.ToString());
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