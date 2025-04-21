using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionData : ScriptableObject
{
    // 条件を満たしているかを判定する抽象メソッド
    public abstract bool IsConditionMet(ItemButton itemButton);
}
