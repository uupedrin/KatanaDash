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
    int previousBlock;
    int currentBlock;
    int blocknumber;

    void Start()
    {
        blocknumber = 0;
        previousBlock = 0;
        currentBlock = 0;
    }

    public void Rearrange()
    {
        int blockposition = (int) (player.transform.position.x / blockSize);
        while(blocknumber == previousBlock || blocknumber == currentBlock) 
        {
            if(!GameManager.manager.bossFight) blocknumber = Random.Range(0,3);
            else blocknumber = Random.Range(3,6);
        }
        previousBlock = currentBlock;
        currentBlock = blocknumber;
        Blocks[currentBlock].transform.position = new Vector3((blockposition + 2) * blockSize, 0, 0);
        Debug.Log(Blocks[previousBlock]);
        for(int i = 0; i < Blocks[previousBlock].transform.childCount; i++)
        {
            Blocks[previousBlock].transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
