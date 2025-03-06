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
        // 所持金をロード
        wallet.money = saveData.LoadMoney();
        // wallet.money = BigInteger.Parse(PlayerPrefs.GetString("MONEY","0"));
        // 全ての犬の頭数をロードする。
        for(var index = 0; index < shop.dogButtonList.Count; index ++ )
        {
            var dogButton = shop.dogButtonList[index];
            var dogCnt = saveData.LoadDogCnt(index);
            dogButton.currentCnt = dogCnt;
            for(var i = 0; i < dogCnt; i++)
            {
                dogButton.dogGenerator.CreateDog(dogButton.dogdata);
            }

        }
        Debug.Log("ロード");
    }

}
