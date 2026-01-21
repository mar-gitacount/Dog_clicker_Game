using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class StoryData : ScriptableObject
{
    public int[] sotryTexts;
    // 敵の種類配列
    public int[] enamys;
    
    // ストーリーテキストに対応するサウンド配列
    public string[] SoundWithDataText;

}
