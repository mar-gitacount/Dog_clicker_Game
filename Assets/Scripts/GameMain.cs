using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    private Button sellButton;

    [SerializeField]
    private Wallet wallet;
  
    // 売却ボタンを押下した時に呼ばれる関数,表示されている毛を全て取得し、所持金に追加する。
    private void SellAllWool()
    {
        // 売却ボタンが動かないのでテスト
        Debug.Log("売却ボタンテスト");
        //画面上の全てのWoolスクリプトが付いたオブジェクトを検索してWool配列woolsに格納
        var wools = FindObjectsByType<Wool>(FindObjectsSortMode.None);
        foreach(var wool in wools)
        {
            wool.sell(wallet);
        }
        SoundManeger.Instance.Play("コイン");

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sellButton == null)
    {
        Debug.LogError("sellButton が設定されていません！");
        return;
    }
        sellButton.onClick.AddListener(SellAllWool);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
