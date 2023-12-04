using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public TMP_Text scoreText;
	// public GameObject pauseMenu;
	// public GameObject config;
	// public GameObject gameUi;
	// private bool isPause;

	private void Start()
	{
		GameManager.manager.UiManager = this;
	}
	
	// public void Pause()
	// {
	// 	if (Input.GetKeyDown(KeyCode.P))
	// 	{
	// 		isPause = !isPause;
	// 		if (isPause)
	// 		{
	// 			Time.timeScale = 0;
	// 			pauseMenu.gameObject.SetActive(true);
	// 			gameUi.gameObject.SetActive(false);
	// 		}
	// 		else
	// 		{
	// 			Time.timeScale = 1;
	// 			pauseMenu.gameObject.SetActive(false);
	// 			gameUi.gameObject.SetActive(true);
	// 		}

	// 	}
	// }
	public void SetStatus(int score)
	{
		scoreText.text = "Score: " + score;
	}
	public void Quit()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
	public void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
}
