using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform emptySprite;
    
    [SerializeField]
    private int height = 30, width = 10, header = 8;

    private void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        if (emptySprite)
        {
            for (int y = 0; y < height - header; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity);
                    clone.transform.parent = transform;
                }
            }
        }
    }

     // ブロックが枠内にあるのか判定
     public bool CheckPosition(Block block)
     {
         // 親Blockは複数個のBlockオブジェクトを組み合わせて
         // I型やL型などの形にしているため
         // 子Blockの位置をforeachで全件枠内に収まっているかチェックする
         foreach (Transform item in block.transform)
         {
             // xy座標が少数点で返ってくる可能性があるため整数に丸める処理
             Vector2 pos = Rounding.Round(item.position);

             int x = (int)pos.x;
             int y = (int)pos.y;
             if (!BoardOutCheck(x, y))
             {
                 // 枠内からはみ出ている場合
                 return false;
             }
         }
         return true;
     }

     bool BoardOutCheck(int x, int y)
     {
         // x軸は0以上、width未満（横が収まっているか）、y軸は0以上（縦が収まっているか）
         return (x >= 0 && x < width && y >= 0);
     }
}
