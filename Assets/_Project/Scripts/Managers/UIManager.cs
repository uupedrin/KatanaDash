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
	public GameObject achivmentsMenu;
	public GameObject creditsMenu;
	public GameObject endGameMenu;
	public GameObject exitMenu;
	public GameObject cheatsMenu;
	public bool isPaused;
    bool[] shown = new bool[7];
    public Image[] images = new Image[7];
    private void Start()
	{
		GameManager.manager.UiManager = this;
		if(pauseMenu != null) pauseMenu.SetActive(false);
		if (configMenu != null) configMenu.SetActive(false);
        for (int i = 0; i < shown.Length; i++)
        {
            if (GameManager.manager.data.achievement[i] == true)
            {
                shown[i] = true;
            }
        }
    }
	
	public void SetCoins(int coins)
	{
		coinText.text = coins.ToString();
	}
	public void SetStatus(float metersRan)
	{
		if(metersText == null) return;
		metersText.text = metersRan + " m";
	}
	public void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
	public void SetCheats() 
	{
        if (cheatsMenu.activeInHierarchy == false)
        {
            cheatsMenu.SetActive(true);
        }
        else
        {
            cheatsMenu.SetActive(false);
        }
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
    public void SetAchivmentsMenu()
    {
        if (achivmentsMenu.activeInHierarchy == false)
        {
            achivmentsMenu.SetActive(true);
        }
        else
        {
            achivmentsMenu.SetActive(false);
        }
    }
	public void SetCreditsMenu() 
	{
        if (creditsMenu.activeInHierarchy == false)
        {
            creditsMenu.SetActive(true);
        }
        else
        {
            creditsMenu.SetActive(false);
        }
    }
    public void SetExitMenu()
    {
        if (exitMenu.activeInHierarchy == false)
        {
            exitMenu.SetActive(true);
        }
        else
        {
            exitMenu.SetActive(false);
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
		finalMetersText.text = finalMeters + " m";
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
    public void PopUp(int i)
    {
        if (!shown[i])
        {
            images[i].gameObject.SetActive(true);
			shown[i] = true;
        }
        StartCoroutine(RemovePopUp(i));
    }
    IEnumerator RemovePopUp(int i)
    {
        yield return new WaitForSeconds(3);
        images[i].gameObject.SetActive(false);
    }
}