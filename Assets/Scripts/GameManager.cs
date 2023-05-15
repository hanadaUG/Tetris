using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private Block activeBlock;

    [SerializeField] private float dropInterval = 0.25f;
    private float _nextDropTimer;

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
        // インターバルに設定した時間が経過したら生成されたブロックを自動落下させる
        if (Time.time > _nextDropTimer)
        {
            // タイマー更新
            _nextDropTimer = Time.time + dropInterval;

            // 落下
            if (activeBlock)
            {
                activeBlock.MoveDown();
            }
        }
    }
}
