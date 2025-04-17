using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDataUse : MonoBehaviour
{
    // ショップの一覧を作成
    // [SerializeField]Shop shop;
    // デモ用犬データ一覧
    // [SerializeField]private DogButton dogButtonPrefab;
    public List<DogButton> dogButtonList;
    // [SerializeField]private DogGenerator dogGenerator;


    // すでに8個のデータが用意されている。
    public DogData[] dogDatas;
    public DogGenerator dogGenerator;
    // 犬購入ボタンプレハブ
    [SerializeField]private DogButton dogbuttonPrefab;
    // 所持金オブジェクト
    [SerializeField]private Wallet wallet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //?テストコード 初期化処理。ドッグボタンプレハブに追加する。
        //?ドッグボタンプレハブ→Walletとdogdataを設定する必要がある。
        foreach(var dogData in dogDatas)
        {
            // 犬ボタンのプレハブ初期化
            var dogButton = Instantiate(dogbuttonPrefab,transform);
            dogButton.dogdata = dogData;
            dogButtonList.Add(dogButton);
            dogButton.dogGenerator = dogGenerator;
            dogButton.wallet = wallet;  
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
