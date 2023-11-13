using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    float blockSize;
    [SerializeField]
    GameObject Block1;
    [SerializeField]
    GameObject Block2;
    [SerializeField]
    GameObject Block3;
    [SerializeField]
    GameObject player;
    int currentBlock;
    int blocknumber;
    
    // Start is called before the first frame update
    void Start()
    {
        blocknumber = 0;
        currentBlock = 0;
    }

    public void Rearrange()
    {
        Debug.Log("oiiiiiii");
        int blockposition = (int)(player.transform.position.x / blockSize);
        while(blocknumber == currentBlock) blocknumber = Random.Range(1,4);
        currentBlock = blocknumber;
        switch(blocknumber)
        {
            case 1:
            Block1.transform.position = new Vector3((blockposition + 1) * blockSize, 0, 0);
            break;
            case 2:
            Block2.transform.position = new Vector3((blockposition + 1) * blockSize, 0, 0);
            break;
            case 3:
            Block3.transform.position = new Vector3((blockposition + 1) * blockSize, 0, 0);
            break;
        }
    }
}
