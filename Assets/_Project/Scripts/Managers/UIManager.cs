using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text metersText;
	public Text finalMetersText;
	public Text coinText;
	public Text finalCoinText;
	public GameObject pauseMenu;
	public GameObject configMenu;
	public GameObject endGameMenu;
	public bool isPaused;
	private void Start()
	{
		GameManager.manager.UiManager = this;
		if(pauseMenu != null) pauseMenu.SetActive(false);
        if (configMenu != null) configMenu.SetActive(false);
    }
	
	public void SetCoins(int coins)
	{
		coinText.text = coins.ToString();
	}
	public void SetStatus(float metersRan)
	{
		metersText.text = metersRan + " m";
	}
	public void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
    public void SetConfigMenu()
    {
		if(configMenu.activeInHierarchy == false) 
		{
			configMenu.SetActive(true);
		}
		else 
		{
			configMenu.SetActive(false);
		}
    }
    public void PauseGame()
	{
		if(isPaused) 
		{
			ResumeGame();
		}
		else 
		{
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
		isPaused = false;
	}
	public void EndGameScreen(float finalMeters, int coins)
	{
        Time.timeScale = 0f;
        isPaused = true;
        finalMetersText.text = finalMeters.ToString();
		finalCoinText.text = coins.ToString();
		endGameMenu.SetActive(true);		
	}
    public void ResetGame()
    {
        GameManager.manager.coins = 0;
        GameManager.manager.metersRan = 0;
        GameManager.manager.finalMeters = 0;
        GameManager.manager.metersTimer = 0;
    }
    public void Quit()
	{
		Application.Quit();
	}
}