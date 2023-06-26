using System;
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

    // 入力受付タイマー
    private float _nextKeyDownTimer, _nextKeyLeftRightTimer, _nextKeyRotateTimer;

    // 入力インターバル
    [SerializeField]
    private float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Spawnerがアタッチされているゲームオブジェクトを探す
        spawner = GameObject.FindObjectOfType<Spawner>();

        // Boardを変数に格納する
        _board = GameObject.FindObjectOfType<Board>();

        // ブロック位置を補正
        spawner.transform.position = Rounding.Round(spawner.transform.position);

        // タイマーの初期化
        _nextKeyDownTimer = Time.time + nextKeyDownInterval;
        _nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        _nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

        if (!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // // インターバルに設定した時間が経過したら生成されたブロックを自動落下させる
        // if (Time.time > _nextDropTimer)
        // {
        //     // タイマー更新
        //     _nextDropTimer = Time.time + dropInterval;
        //
        //     // 落下
        //     if (activeBlock)
        //     {
        //         activeBlock.MoveDown();
        //
        //         // 枠内からBlockがはみ出ていないか確認
        //         if (!_board.CheckPosition(activeBlock))
        //         {
        //             // はみ出ていたら落下
        //             activeBlock.MoveUp();
        //
        //             // ブロック位置保存
        //             _board.SaveBlockInGrid(activeBlock);
        //
        //             activeBlock = spawner.SpawnBlock();
        //         }
        //     }
        // }

        PlayerInput();
    }

    // キー入力
    void PlayerInput()
    {
        // キー押しっぱなし（インターバル中は受け付けない） 、キー押した瞬間
        if (Input.GetKey(KeyCode.D) && (Time.time > _nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.D))
        {
            activeBlock.MoveRight();
            _nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            // 移動できない場合は元の位置に戻す
            if (!_board.CheckPosition(activeBlock))
            {
                activeBlock.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > _nextKeyLeftRightTimer) || Input.GetKeyDown(KeyCode.A))
        {
            activeBlock.MoveLeft();
            _nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;

            // 移動できない場合は元の位置に戻す
            if (!_board.CheckPosition(activeBlock))
            {
                activeBlock.MoveRight();
            }
        }
        else if (Input.GetKey(KeyCode.E) && (Time.time > _nextKeyRotateTimer) || Input.GetKeyDown(KeyCode.E))
        {
            activeBlock.RotateRight();
            _nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            // 移動できない場合は元の位置に戻す
            // if (!_board.CheckPosition(activeBlock))
            // {
            //     activeBlock.RotateLeft();
            // }
        }
        else if (Input.GetKey(KeyCode.Q) && (Time.time > _nextKeyRotateTimer) || Input.GetKeyDown(KeyCode.Q))
        {
            activeBlock.RotateLeft();
            _nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            // 移動できない場合は元の位置に戻す
            // if (!_board.CheckPosition(activeBlock))
            // {
            //     activeBlock.RotateRight();
            // }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > _nextKeyDownTimer) || Time.time > _nextDropTimer)
        {
            activeBlock.MoveDown();
            _nextKeyDownTimer = Time.time + nextKeyDownInterval;

            // 自動落下のタイマー更新
            _nextDropTimer = Time.time + dropInterval;

            if (!_board.CheckPosition(activeBlock))
            {
                // ブロックが着地した
                BottomBoard();
            }
        }
    }

    // ブロックが着地した後、次のブロックを生成
    void BottomBoard()
    {
        activeBlock.MoveUp(); // ブロックを元の位置に戻す
        _board.SaveBlockInGrid(activeBlock); // 位置を保存

        activeBlock = spawner.SpawnBlock(); // 次のブロックを生成

        // タイマーの初期化
        _nextKeyDownTimer = Time.time;
        _nextKeyLeftRightTimer = Time.time;
        _nextKeyRotateTimer = Time.time;
    }
}
