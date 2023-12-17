using System;
using System.Collections;
using System.IO;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] public int coins = 0;
	[SerializeField] public float metersRan;
	[SerializeField] public float metersTimer;
	[SerializeField] public float finalMeters;
	public static GameManager manager;
	public UIManager UiManager;
	public AchievmentsPopUp AchievmentsPopUp;
	public float freezeDuration;
	[SerializeField] public Achievements data;
	public float saveMasterSlider;
	public float saveMusicSlider;
	public float saveSfxSlider;
	public bool bossFight;
	public bool immortal = false;
	public bool damageCheat = false;
	public GameObject bossTrigger;

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
		data = new Achievements();
		LoadFromJson();
		DontDestroyOnLoad(gameObject);
	}
	
	private void Update()
	{
		metersTimer += Time.deltaTime;
		metersRan = 2.08f * metersTimer;
		metersRan = (float)Math.Round(metersRan, 1);
		if (finalMeters == 0)
		{
			UiManager.SetStatus(metersRan);
		}
		if (coins == 20) 
		{
			SetAchievement(1);
			UiManager.PopUp(1);
		}
		if(coins == 100) 
		{
			SetAchievement(2);
			UiManager.PopUp(2);
		}
		if (metersRan == 1000) 
		{
			SetAchievement(4);
			UiManager.PopUp(4);
		}
	}

	public void AddPoints(int enemyType)
	{
		switch (enemyType)
		{
			case 1: // coins
				coins += 1;
				data.achievement[0] = true;
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

	public void SetAchievement(int achievementID)
	{
		bool lastState;
		lastState = data.achievement[achievementID];
		data.achievement[achievementID] = true;
		SaveToJson();
		if(!lastState)
		{
			for (int i = 0, aux = 0; i < data.achievement.Length-1; i++) 
			{
				if (data.achievement[i]) aux++;
				
				if(aux == 5)
				{
					SetAchievement(6);
					UiManager.PopUp(6);
				}
			}
		}
	}

	[ContextMenu("Save")]
	public void SaveToJson()
	{
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(Application.persistentDataPath + "/AchievementsFile.json", json);
	}

	public void LoadFromJson()
	{
		string json = "";
		if(File.Exists(Application.persistentDataPath + "/AchievementsFile.json"))
		{
			json = File.ReadAllText(Application.persistentDataPath + "/AchievementsFile.json");
			data = JsonUtility.FromJson<Achievements>(json);
		}
		else
		{
			SaveToJson();
			LoadFromJson();
		}
	}

	public void BossCheat()
	{
		if(!bossFight) bossTrigger.transform.position = new UnityEngine.Vector3(Camera.main.gameObject.transform.position.x + 5, 0, 0);
	}

	public void InvulnerableCheat()
	{
		immortal = !immortal;
		if(immortal)
		{
			UiManager.ToggleShieldImage(true);
			UiManager.ToggleShieldCheatSide(true);
		}
		else
		{
			UiManager.ToggleShieldImage(false);
			UiManager.ToggleShieldCheatSide(false);
		}
	}

	public void DamageCheat()
	{
		damageCheat = !damageCheat;
		if(damageCheat) UiManager.ToggleDamageCheatSide(true);
		else
		{
			UiManager.ToggleDamageCheatSide(false);
		}
	}
}
/*
ACHIEVEMENTS:
0 - First Dash
1 - Collected 10 coins in one run
2 - Collected 30 total coins
3 - Died while in the air
*/