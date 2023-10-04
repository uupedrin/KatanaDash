using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject pauseMenu;
    public GameObject config;
    public GameObject gameUi;
    private bool isPause;

    void Update()
    {
        Pause();
        SetStatus();
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                pauseMenu.gameObject.SetActive(true);
                gameUi.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.gameObject.SetActive(false);
                gameUi.gameObject.SetActive(true);
            }

        }
    }
    public void SetStatus()
    {
        scoreText.text = "Score: " + GameManager.manager.score;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ChangeScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}
