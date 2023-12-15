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
    int numberOfBlocks;
    int currentBlock;
    int blockNumber;
    int previousBlock;

    void Start()
    {
        blockNumber = 0;
        currentBlock = 0;
        previousBlock = 0;
        numberOfBlocks = Blocks.Length;
    }

    public void Rearrange()
    {
        int blockposition = (int)(player.transform.position.x / blockSize);
        while(blockNumber == currentBlock || blockNumber == previousBlock) 
        {
            if(!GameManager.manager.bossFight) blockNumber = Random.Range(0, numberOfBlocks - 3);
            else blockNumber = Random.Range(numberOfBlocks - 3, numberOfBlocks);
        }
        previousBlock = currentBlock;
        currentBlock = blockNumber;
        Blocks[currentBlock].transform.position = new Vector3((blockposition + 2) * blockSize, 0, 0);
        for(int i = 0; i < Blocks[currentBlock].transform.childCount; i++)
        {
            Blocks[currentBlock].transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}