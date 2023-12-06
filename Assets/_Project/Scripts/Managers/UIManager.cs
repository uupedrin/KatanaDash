using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public TMP_Text scoreText;
	public GameObject pauseMenu;
    public static bool isPaused;
	private void Start()
	{
		GameManager.manager.UiManager = this;
		pauseMenu.SetActive(false);
	}
	public void SetStatus(int score)
	{
		scoreText.text = "Score: " + score;
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
    public void Quit()
    {
        Application.Quit();
    }
}