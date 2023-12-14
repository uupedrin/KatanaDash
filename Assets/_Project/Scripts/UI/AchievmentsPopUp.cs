using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentsPopUp : MonoBehaviour
{
    bool[] shown = new bool[7];
    public Image[] images = new Image[7];
    private void Start()
    {
        for (int i = 0; i < shown.Length; i++) 
        {
            if (GameManager.manager.data.achievement[i] == true) 
            {
                shown[i] = true;
            }
        }
    }
    public void PopUp(int i) 
    {
        if (!shown[i]) 
        {
            images[i].gameObject.SetActive(true);
        }
        StartCoroutine(RemovePopUp(i));
    }
    IEnumerator RemovePopUp(int i) 
    {
        yield return new WaitForSeconds(3);
        images[i].gameObject.SetActive(false);
    }
}