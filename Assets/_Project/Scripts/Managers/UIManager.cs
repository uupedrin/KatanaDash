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
	public GameObject endGameMenu;
	public bool isPaused;
	private void Start()
	{
		GameManager.manager.UiManager = this;
		if(pauseMenu != null) pauseMenu.SetActive(false);
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
	public void PauseGame()
	{
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
		isPaused = true;
	}
	public void ResumeGame()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
		isPaused = false;
	}
	public void EndGameScreen(float finalMeters, int coins)
	{
		finalMetersText.text = finalMeters.ToString();
		finalCoinText.text = coins.ToString();
		endGameMenu.SetActive(true);		
	}
	public void Quit()
	{
		Application.Quit();
	}
}