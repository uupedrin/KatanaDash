using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Achievements
{
    public bool[] achievement = new bool[7];
    void Update() 
    {
        for (int i = 0; i < achievement.Length-1; i++) 
        {
            int aux = 0;
            if (achievement[i] == true) 
            {
                aux++;
            }
        }
    }
}