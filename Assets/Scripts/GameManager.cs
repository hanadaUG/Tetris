using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private Block activeBlock;

    // Start is called before the first frame update
    void Start()
    {
        // Spawnerがアタッチされているゲームオブジェクトを探す
        spawner = GameObject.FindObjectOfType<Spawner>();

        if (!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
