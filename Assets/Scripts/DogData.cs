using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[CreateAssetMenu]

// 犬データの動きの基本設計コード?
public class DogData : ScriptableObject
{
    // ?犬の画像
    [SerializeField]private SpriteRenderer sheepRenderder;
    // ?犬の種類
    public string dogkinds;
    // 以下を犬のデータにする。
    public Color color;
    // 初期値段
    public int basePrice;
    // 値段上昇額
    public int extendetPrice;
    // 購入上限数
    public int maxCount;
    // 毛の量
    public int woolCnt;

}
