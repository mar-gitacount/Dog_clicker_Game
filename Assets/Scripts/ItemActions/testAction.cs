using UnityEngine;

[CreateAssetMenu(menuName = "ItemActions/TestAction")]
public class TestAction : ActionData
{
    public override void Execute(ItemButton itemButton)
    {
        // ここにアクションの実行内容を記述
        Debug.Log("TestAction executed!");
        // itemButton.wallet.money -= itemButton.itemData.basePrice;
        // itemButton.DisableButton();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Sheep script = obj.GetComponent<Sheep>();
            if(script != null)
            {
                script.Shaving();
            }
            // Debug.Log($"{wallet.money}は関数内で料金を確認する。");
            Debug.Log($"{obj.name}はオブジェクトを取得する。");
        }
    }
}