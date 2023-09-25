using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager manager;
    void Start()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        score = 0;
    }

    //Adiciona pontuação baseado no tipo de inimigo, li na net que nosso cérebro de primata gosta numeros grandes. "Big number GoOg"
    public void AddPoints(int enemyType)
    {
        switch (enemyType)
        {
            case 1:
                score += 500;
                break;
            case 2:
                score += 1000;
                break;
            case 3:
                score += 1500;
                break;
        }
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
