using UnityEngine;

[CreateAssetMenu(menuName = "ItemConditions/WoolTenCount")]
public class WoolTenCount : ConditionData
{
    public override bool IsConditionMet(ItemButton itemButton)
    {
        // シーン内のすべてのWoolコンポーネントを取得
        Wool[] woolObjects = Object.FindObjectsOfType<Wool>();

        // Woolの数を確認
        int woolCount = woolObjects.Length;

        Debug.Log($"Woolの数: {woolCount}");

        // Woolが10個以上存在する場合はtrueを返す
        // return true;
        return woolCount >= 10;
    }
}