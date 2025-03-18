using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    
    [SerializeField]
    private Button sellButton;

    [SerializeField]
    private Wallet wallet;
  
    private bool isPaused = false;
    public GameObject pauseMenu;
    // 売却ボタンを押下した時に呼ばれる関数,表示されている毛を全て取得し、所持金に追加する。
    private void SellAllWool()
    {
       
        //画面上の全てのWoolスクリプトが付いたオブジェクトを検索してWool配列woolsに格納。
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
        // 以下でゲーム開始ボタンを実装する？？
        Debug.Log("ゲームスタート");
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
        Debug.Log("メインのアップデートテスト");
        // ?以下をボタンに変えなければならない。
         if(Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        pauseMenu.SetActive(isPaused);
        // EventSystem.current.enabled = !isPaused;
    }
}
