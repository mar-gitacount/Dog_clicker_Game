using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DogButton : MonoBehaviour
{
    [SerializeField]private Button button;
    public DogData dogdata;
    public DogGenerator dogGenerator;

    //! 犬画像複数あるので、あとで配列化させ参照する。
    // !以下が犬画像の根本画像。
    [SerializeField] private Image dogImage;
    // 金額テキスト
    [SerializeField] private Text priceText;
    // 頭数テキスト
    [SerializeField] private Text countText;
    // 購入可否テキスト
    [SerializeField] private Text infoText;
    // 所持金Text
    public Wallet wallet;
    // 現在の頭数
    public int currentCnt;
    // 現在の犬の金額を返却
    public int GetPrice()
    {
        // 現在の頭数から、次の購入金額を計算する。
        return dogdata.basePrice + dogdata.extendetPrice * currentCnt;
    }
    public void CreateDog()
    {
        // ここで犬を召喚している。
        dogGenerator.CreateDog(dogdata);

        // 現在の頭数から、次の購入金額を計算する。
        var price = GetPrice();
        // 購入した分、所持金からマイナス
        wallet.money -= price;
        currentCnt ++;
        SoundManeger.Instance.Play("ワン");

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 犬を生成するメソッドを実行する。種類は参照先で選択する。
        button.onClick.AddListener(CreateDog);
        
    }

    // Update is called once per frame
    void Update()
    {

        // 現在の頭数から次の購入金額を計算。
        var price = GetPrice();
        // ?犬の色をセットもしかして消すかも→そもそも画像も動的にしなければならない。
        Debug.Log($"{dogdata.dogkinds}は犬の種類これを元にセットする。");
        // 犬の種類の変数
        var dogkinds = dogdata.dogkinds;
        var picPath = dogdata.picturePath;
        // !画像差し替え。
        Sprite newSprite = Resources.Load<Sprite>("Images/"+picPath);
        //  Sprite newSprite = Resources.Load<Sprite>("Images/ビションフリーゼ");

        // ?あとで消す、リソースファイル内の、イメージが存在するか確認する。
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images");
        foreach(Sprite sprite in sprites)
        {
            Debug.Log("スプライト名"+sprite.name);
            if(sprite.name == dogkinds)
            {
                Debug.Log($"{sprite.name}と{dogkinds}は同じ");
                
            }
        }

        if(newSprite != null)
        {
            // ↓はヒエラルキー内の画像に新しい画像をセットしている
            Debug.Log($"{newSprite}");
            dogImage.sprite = newSprite;
        }
        else
        {
            // デフォルトの画像になる。
            Debug.LogError("スプライトが見つかりません: " + newSprite);
        }
        dogImage.color = dogdata.color;
        // 金額表示なので、"C0"をセットする。
        // priceText.text = price.ToString("C0");
        priceText.text = price.ToString();

        // 現在の頭数と上限値。

        countText.text = $"{currentCnt}頭/{dogdata.maxCount}頭";
        if(currentCnt >= dogdata.maxCount)
        {
            infoText.text = "完売";
            button.interactable = false;
        }
        // 所持金が上
        else if (wallet.money >= price)
        {
            infoText.text = "購入";
            button.interactable = true;

        }
        else
        {
            infoText.text = "お金が足りません";
            button.interactable = false;
        }
    }
}
