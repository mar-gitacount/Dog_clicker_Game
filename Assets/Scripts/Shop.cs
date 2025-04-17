using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // 購入ボタンプレハブ
    [SerializeField]private DogButton dogbuttonPrefab;

    // 生成元になる犬データ配列
    public DogData[]  dogDatas;
    // 作成したショップ犬ボタンをListで保持する。
    public List<DogButton> dogButtonList;
    // 犬ボタンにセットする犬生成オブジェクト
    [SerializeField]private DogGenerator dogGenerator;
    // 犬ボタンにセットする、所持金オブジェクト
    [SerializeField]private Wallet wallet;
    
    void Awake()
    {
        // ここでボタンスクリプトに渡す?
        foreach(var dogData in dogDatas)
        {
            // 犬の画像を動的にする。
            // 合計八回ループする。
            // 犬データのインスタンスを作成。犬ボタンの一覧に追加。値段も追加。
            Debug.Log($"{dogData}ドッグデータチェック");
            var dogButton = Instantiate(dogbuttonPrefab, transform);   
            dogButton.dogdata = dogData;
            dogButtonList.Add(dogButton);
            dogButton.dogGenerator = dogGenerator;
            dogButton.wallet = wallet;
        }
        
    }

    // Update is called once per frame
}
