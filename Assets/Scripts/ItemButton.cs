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
      public int GetPrice()
    {
        // 現在の頭数から、次の購入金額を計算する。
        // return itemData.basePrice + dogdata.extendetPrice * currentCnt;
        return itemData.basePrice;
    }
    public void Click()
    {
        if(button.interactable == false) return;
        // アイテムの関数実行。
        itemData.InvokeFunction(this);
    }
    public void DisableButton()
    {
        // アイテムの関数実行。
        button.interactable = false;
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
        var price = GetPrice();
        // priceText.text = price.ToString("C0");
        priceText.text = price.ToString();
        //? 
        infoText.text = "仮";
        // 画像データのパスを取得する。
        var picPath = itemData.picturePath;

        Sprite newSprite = Resources.Load<Sprite>("Images/"+picPath);

        if(newSprite != null)
        {
            itemimage.sprite = newSprite;
        }
        else
        {
            Debug.LogError($"画像が見つかりません: {picPath}");
        }

        // 画像の場合、item.Data.picturePathを取得
        Debug.Log($"アイテムボタンの画像情報は、{itemData.picturePath}です。");
        if(wallet == null)
        {
            Debug.LogError("Walletが設定されていません！");
            return;
        }
        if(wallet.money <= price)
        {
            button.interactable = false;
        }
        else if (wallet.money >= price)
        {
            button.interactable = true;
        }
        
        
    }
}
