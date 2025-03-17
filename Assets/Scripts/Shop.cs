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
        // 受け取ったDogDataの数だけDogButtonを作成。
        // !ここでボタンスクリプトに渡す?
        // ゲームスタート時に犬データの数だけループして、ボタンを作る
        foreach(var dogData in dogDatas)
        {
            var dogButton = Instantiate(dogbuttonPrefab, transform);   
            dogButton.dogdata = dogData;
            dogButtonList.Add(dogButton);
            dogButton.dogGenerator = dogGenerator;
            dogButton.wallet = wallet;
        }
        
    }

    // Update is called once per frame
}
