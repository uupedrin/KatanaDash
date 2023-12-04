using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public int score = 0;
	public static GameManager manager;
	public UIManager UiManager;
	public float freezeDuration;
	public bool[] achievement;
	void Awake()
	{
		if(!File.Exists("coinsrecord.json") || Convert.ToInt32(File.ReadAllText("coinsrecord.json")) < 10) File.WriteAllText("coinsrecord.json", "0");
		if(!File.Exists("coinstotal.json")) File.WriteAllText("coinstotal.json", "0");
		HighScore();
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
				File.WriteAllText("coinsrecord.json", (Convert.ToInt32(File.ReadAllText("coinsrecord.json"))+1).ToString());
				File.WriteAllText("coinstotal.json", (Convert.ToInt32(File.ReadAllText("coinstotal.json"))+1).ToString());
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

	public void Achievements(float dashed)
	{
		if(dashed > 0) achievement[0] = true;
		if(Convert.ToInt32(File.ReadAllText("coinsrecord.json")) >= 10) achievement[1] = true;
		if(Convert.ToInt32(File.ReadAllText("coinstotal.json")) >= 30) achievement[2] = true;
        if (Convert.ToInt32(File.ReadAllText("coinstotal.json")) >= 100) achievement[3] = true;

    }

    void DeleteSave()
	{
		File.WriteAllText("highscore.json", "0");
		for(int i = 0; i < achievement.Length; i++)
		{
			achievement[i] = false;
		}
		File.Delete("coinstotal.json");
		File.Delete("coinsrecord.json");
	}
}
