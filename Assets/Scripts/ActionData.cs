using UnityEngine;

public abstract class ActionData : ScriptableObject
{
    // アクションを実行する抽象メソッド
    public abstract void Execute(ItemButton itemButton);
}
