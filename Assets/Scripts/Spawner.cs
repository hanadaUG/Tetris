using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Block[] Blocks;

    Block GetRandomBlock()
    {
        int i = Random.Range(0, Blocks.Length);
        if (Blocks[i])
        {
            return Blocks[i];
        }

        return null;
    }

    public Block SpawnBlock()
    {
        Block block = Instantiate(GetRandomBlock(), transform.position, Quaternion.identity);
        if (block)
        {
            return block;
        }

        return null;
    }
}

