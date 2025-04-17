using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField]private Button button;
    // 金額テキスト
    [SerializeField]private Text priceText;
    // アイテム数(仮)
    [SerializeField] private Text coutnText;
    // 利用可否及び購入テキスト
    [SerializeField]private Text infoText;
    // アイテムボタンの画像
    // ドッグデータ内に写真が設定されていた場合、それを代入する。
    [SerializeField]private Image itemimage;
    // アイテムの能力や画像、料金の特性が格納されている。
    public ItemData itemData;
    // public ItemGenerator itemGenerator;
    public Wallet wallet;
    public int currentCnt;

    // 
    public void CreateItem()
    {
        // ここから呼び出して、呼び出し先で効果を付与する。

        // itemGenerator.CreateItem(itemData);
        
    }
    public void Click()
    {
        // アイテムの関数実行。
        itemData.InvokeFunction();
    }
    void Start()
    {
        // ここでアイテムを生成するメソッドを付与する。
        button.onClick.AddListener(Click);

    }

    // Update is called once per frame
    void Update()
    {
        // button.onClick.AddListener(Click);
        //? 値段(仮)
        var price = 0;
        priceText.text = price.ToString("C0");
        //? 
        infoText.text = "仮";
        
    }
}
