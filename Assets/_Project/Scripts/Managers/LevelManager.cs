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
    bool bossfight = false;

    void Start()
    {
        blocknumber = 0;
        currentBlock = 0;
    }

    public void Rearrange()
    {
        int blockposition = (int)(player.transform.position.x / blockSize);
        while(blocknumber == currentBlock) 
        {
            if(!bossfight) blocknumber = Random.Range(0,3);
            else blocknumber = Random.Range(3,4);
        }
        currentBlock = blocknumber;
        Blocks[currentBlock].transform.position = new Vector3((blockposition + 1) * blockSize, 0, 0);
        Debug.Log(Blocks[currentBlock]);
        for(int i = 0; i < Blocks[currentBlock].transform.childCount; i++)
        {
            Blocks[currentBlock].transform.GetChild(i).gameObject.SetActive(true);
        }
        
    }
}
