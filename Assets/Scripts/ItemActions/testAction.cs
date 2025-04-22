using UnityEngine;
// using Game.Managers; // SheepManagerが属する名前空間を指定
// 画面上の犬の毛をすべて刈るアクションを実行するクラス
// 毛を全て掃除する。
using System.Collections.Generic; 
[CreateAssetMenu(menuName = "ItemActions/TestAction")]
public class TestAction : ActionData
{
    public override void Execute(ItemButton itemButton)
    {
        Debug.Log("TestAction executed!");

        if (SheepManager.Instance == null)
        {
            Debug.LogError("SheepManager.Instance is null. Make sure SheepManager is in the scene.");
            return;
        }

        List<Sheep> sheepList = SheepManager.Instance.GetAllSheep();
        foreach (Sheep sheep in sheepList)
        {
            sheep.Shaving();
        }

        Debug.Log($"Sheep count: {sheepList.Count}");
    }
}