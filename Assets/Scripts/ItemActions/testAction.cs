using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ItemActions/TestAction")]
public class TestAction : ActionData
{
    private Wallet wallet;

    private void SellAllWool()
    {
        // walletが設定されていない場合、FindObjectOfTypeで取得
        if (wallet == null)
        {
            wallet = FindObjectOfType<Wallet>();
            if (wallet == null)
            {
                Debug.LogError("Walletオブジェクトが見つかりません！");
                return;
            }
        }

        // 画面上の全てのWoolスクリプトが付いたオブジェクトを検索して処理
        var wools = FindObjectsByType<Wool>(FindObjectsSortMode.None);
        if (wools.Length == 0)
        {
            Debug.LogWarning("Woolオブジェクトが見つかりません！");
            return;
        }

        foreach (var wool in wools)
        {
            wool.sell(wallet);
        }

        // サウンドを再生
        SoundManeger.Instance.Play("コイン");
    }

    public override void Execute(ItemButton itemButton)
    {
        Debug.Log("TestAction executed!");

        // SheepManagerの確認
        if (SheepManager.Instance == null)
        {
            Debug.LogError("SheepManager.Instance is null. Make sure SheepManager is in the scene.");
            return;
        }

        // Sheepの毛を刈る処理
        List<Sheep> sheepList = SheepManager.Instance.GetAllSheep();
        foreach (Sheep sheep in sheepList)
        {
            sheep.Shaving();
        }

        Debug.Log($"Sheep count: {sheepList.Count}");

        // 全てのWoolを売る処理を実行
        SellAllWool();
    }
}