using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    private Block activeBlock;

    [SerializeField] private float dropInterval = 0.25f;
    private float _nextDropTimer;

    private Board _board;

    // Start is called before the first frame update
    void Start()
    {
        // Spawnerがアタッチされているゲームオブジェクトを探す
        spawner = GameObject.FindObjectOfType<Spawner>();

        // Boardを変数に格納する
        _board = GameObject.FindObjectOfType<Board>();

        // ブロック位置を補正
        spawner.transform.position = Rounding.Round(spawner.transform.position);

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

                // 枠内からBlockがはみ出ていないか確認
                if (!_board.CheckPosition(activeBlock))
                {
                    // はみ出ていたら落下
                    activeBlock.MoveUp();
                    activeBlock = spawner.SpawnBlock();
                }
            }
        }
    }
}
