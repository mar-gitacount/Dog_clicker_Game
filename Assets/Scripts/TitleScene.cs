using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleSchene : MonoBehaviour
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
    [SerializeField]private Button button;
    public void CreateDog()
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
        // スタートボタンのイベントリスナーを設定する。
       
    }
    void Start()
    {
        button.onClick.AddListener(ChangeScene);
         foreach(var dogData in dogDatas)
        {
            var dogCnt = 10;
            for (var i = 0; i < dogCnt; i++ )
            {
                dogGenerator.CreateDog(dogData);

            }
            
        }
    }

    // ゲームシーンへチェンジする関数。
    public void ChangeScene()
    {
        // ユーザー登録していた場合、ユーザー画面遷移
        SceneManager.LoadScene("MainScene");
        // ログインしてない場合、ログイン画面遷移
        
    }


}