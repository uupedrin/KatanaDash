using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    float blockSize;
    [SerializeField]
    GameObject[] Blocks;
    [SerializeField]
    GameObject player;
    int currentBlock;
    int blocknumber;

    void Start()
    {
        blocknumber = -1;
        currentBlock = -1;
    }

    public void Rearrange()
    {
        int blockposition = (int)(player.transform.position.x / blockSize);
        Debug.Log(currentBlock);
        while(blocknumber == currentBlock) 
        {
            if(!GameManager.manager.bossFight) blocknumber = Random.Range(0,3);
            else blocknumber = Random.Range(3,Blocks.Length);
        }
        currentBlock = blocknumber;
        Blocks[currentBlock].transform.position = new Vector3((blockposition + 1) * blockSize, 0, 0);
        for(int i = 0; i < Blocks[currentBlock].transform.childCount; i++)
        {
            Blocks[currentBlock].transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }
}
