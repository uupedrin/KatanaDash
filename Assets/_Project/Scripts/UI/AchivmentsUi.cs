using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivmentsUi : MonoBehaviour
{
    public Image coin;
    public Image coins;
    public Image COINS;
    public Image coffin;
    public Image running;
    public Image bossWin;
    public Image platin;
    Achievements data = new Achievements();
    void OnEnable()
    {
        GameManager.manager.LoadFromJson();
        if (GameManager.manager.data.achievement[0] == true) 
        {
            coin.color = Color.yellow;
        }
        if (GameManager.manager.data.achievement[1] == true)
        {
            coins.color = Color.yellow;
        }
        if (GameManager.manager.data.achievement[2] == true)
        {
            COINS.color = Color.yellow;
        }
        if (GameManager.manager.data.achievement[3] == true) 
        {
            coffin.color = Color.red;
        }
        if (GameManager.manager.data.achievement[4] == true)
        {
            running.color = Color.white;
        }
        if (GameManager.manager.data.achievement[5] == true)
        {
            bossWin.color = Color.white;
        }
        if (GameManager.manager.data.achievement[6] == true)
        {
            platin.color = Color.blue;
        }
    }
}