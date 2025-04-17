using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // 保存対象所持金
    [SerializeField]private Wallet wallet;
    // 犬の頭数
    [SerializeField]Shop shop;

    // 犬データを使う
    [SerializeField]DogDataUse dogdatause;
    // セーブロードインターフェイス
    private ISaveData saveData;


    void Awake()
    {
        saveData = new PlayerPrefsSaveData();
        // !デバッグ用
        // saveData = new DebugSaveData();
    }
    private void OnApplicationQuit()
    {
        Debug.Log("セーブ");   
        //所持金を保存する。
        saveData.SaveMoney(wallet.money);
        // PlayerPrefs.SetString("MONEY",wallet.money.ToString());
        // 全ての羊の頭数を保存する。
        for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            var dogButton = shop.dogButtonList[index];
            saveData.SaveDogCnt(index,dogButton.currentCnt);
            // PlayerPrefs.SetInt($"DOG{index}",dogButton.currentCnt);
        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ?以下犬データテスト。
        // for(var index = 0; index < dogDataUse.dogDatas.Count; index++)
        // {
        //     var dogCnt = saveData.LoadDogCnt(index);
        //     // !以下犬(i番目の)数だけループする。
        // }
        // 所持金をロード
        wallet.money = saveData.LoadMoney();
        // wallet.money = BigInteger.Parse(PlayerPrefs.GetString("MONEY","0"));
        // 全ての犬の頭数をロードする。→ショップオブジェクトから引用している。
        // ?犬データ追加テスト
        for(var index = 0; index < dogdatause.dogButtonList.Count; index ++)
        {
            var dogButton = dogdatause.dogButtonList[index];
            var dogCnt = saveData.LoadDogCnt(index);
            dogButton.currentCnt = dogCnt;
            Debug.Log($"{dogCnt}は犬の数、犬データ引用テスト");
            for(var i = 0; i < dogCnt; i++)
            {
                dogButton.dogGenerator.CreateDog(dogButton.dogdata);
            }
        }
        for(var index = 0; index < shop.dogButtonList.Count; index ++ )
        {
            var dogButton = shop.dogButtonList[index];
            var dogCnt = saveData.LoadDogCnt(index);
            dogButton.currentCnt = dogCnt;
            Debug.Log($"{dogCnt}は犬の数");
            for(var i = 0; i < dogCnt; i++)
            {
                // セーブデータの数だけの犬をメソッド作成する。
                dogButton.dogGenerator.CreateDog(dogButton.dogdata);
            }

        }
        Debug.Log("ロード");
    }

}
