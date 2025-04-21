using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System.Reflection;
[CreateAssetMenu]

// 犬データの動きの基本設計コード?
public class ItemData : ScriptableObject
{
    // 関数が実行される条件を設定する。
    [SerializeField] private ConditionData[] conditions;
    // 関数を設定する。
    [SerializeField] private ActionData[] actions;       // アクションリスト

    public bool CanUse(ItemButton itemButton){
        foreach (var condition in conditions)
        {
            if (!condition.IsConditionMet(itemButton))
            {
                return false;
            }
        }
        return true;
    }

    // !メソッドを設定
    [SerializeField] private UnityEvent onAction;
    // !関数名
    [SerializeField] public string functionName;
    public Wallet wallet;
    // 設定した関数
    public void InvokeFunction(ItemButton itemButton)
    {
        //!後で消す
        // if(itemButton.wallet.money < basePrice)
        // {
        //     Debug.Log("お金が足りません。");
        //     itemButton.DisableButton();
        //     return;
        // }
        if(!CanUse(itemButton))
        {
            Debug.Log("条件を満たしていません。");
            itemButton.DisableButton();
            return;
        }
        MethodInfo method = GetType().GetMethod(functionName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        // if(wallet.money < basePrice){
        //     Debug.Log("お金が足りません。");
        //     return;
        // }
        foreach (var action in actions)
        {
            action.Execute(itemButton);
        }
        return;
        if(method != null)
        {
            method.Invoke(this,null);
        }
        else
        {
            Debug.LogWarning($"関数 '{functionName}' が見つかりません！");
        }
    }
    // ?あとで適した名前に変える。今はめんどくさいからあとで
    // 現段階では、アイテムの挙動の関数は全て、このスクリプト内に格納されている。
    private void test()
    {
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
        Debug.Log("テスト関数が実行されました！！");
    }
    private void test2()
    {
        Debug.Log("これはテスト関数2です");
    }
    public void PerformAction()
    {
        onAction?.Invoke();
    }
    // ?犬の画像
    [SerializeField]private SpriteRenderer sheepRenderder;
    // ?犬の種類
    public string dogkinds;
    // 犬の画像パス 
    public string picturePath;

    // 犬の毛を切ったあとの画像パス
    public string cutPicturePath;
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
