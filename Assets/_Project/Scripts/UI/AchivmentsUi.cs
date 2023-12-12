using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivmentsUi : MonoBehaviour
{
    public Image coin;
    Achievements data = new Achievements();
    void OnEnable()
    {
        GameManager.manager.LoadFromJson();
        if (GameManager.manager.data.achievement[0] == true) 
        {
            coin.color = Color.yellow;
        }
    }
}