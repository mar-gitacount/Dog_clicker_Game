using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    [SerializeField]private ItemButton itembuttonPrefab;
    public ItemData[] itemDatas;
    public List<ItemButton> itemButtonList;
    [SerializeField]private Wallet wallet;
    // [SerializeField]private ItemGenerator itemGenerator;
    void Awake()
    {
        foreach(var itemData in itemDatas)
        {
            // !ここでitemData内のメソッドをボタンに渡してみる。
            // itemDataの一意の値が代入されている。
            // アイテムボタンのインスタンスを作成する。
            var itemButton = Instantiate(itembuttonPrefab,transform);
            itemButton.itemData = itemData;
            itemButtonList.Add(itemButton);
            itemButton.wallet = wallet;
            // itemButton.itemGenerator = itemGenerator;
            //! あとで金額設定する
        }
    }
}
