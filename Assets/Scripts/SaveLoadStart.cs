using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SaveLoadStart : MonoBehaviour
{
    [SerializeField] private Button saveLoadButton;
    [SerializeField] private Text buttonText;
    // private SaveLoadManager saveLoadManager;
    private ISaveData saveData;
    private SaveLoadManager saveLoadManager;
    public int saveIndex;
    public int storyIndex;
    public int lotId;
    public int saveTime;
    public string LoadTimeString;
    public SaveData saveDataJson;
    public String money;

    // 時間とか話数とか保存する。
    // Start is called before the first frame update
    void Start()
    {
        // saveLoadManager.JsonLoadFromLocal(1);
        saveData = new PlayerPrefsSaveData();
        
        
        
        // saveLoadManager.JsonLoadFromLocal(0);
        // storyIndex = saveData.LoadStoryProgress();
        // 現在のセーブ番号をロード
        // storyIndex = saveData.LoadNow();
        // 以下がロットナンバー
        saveIndex = int.Parse(buttonText.text);
        // ロットIDを保存する。
        // saveData.SaveLotId(saveIndex);
        // ロットナンバーから引用する。
        // json形式データが返ってくる。
        // storyIndex = saveData.LoadLotId(saveIndex);
        // 現段階のデータをロードする。
        saveDataJson = saveData.JsonLoadFromLocal();
        // 呼び出し元からセーブロット番号を受け取り、参照する
        
        // int storyDataow = saveData.LoadNow();
        // storyIndex = saveDataJson.storyIndex;
        int storyDataow = saveData.JsonLoadFromLocal(saveIndex).storyIndex;
        money = saveData.JsonLoadFromLocal(saveIndex).money;
        // ボタンテキスト=ロットナンバー
        // buttonText.text = buttonText.text + "ストーリー" + storyData;
        // テキストからロットナンバーを使って保存されている時間を引用する。
        LoadTimeString = saveData.LoadTime(saveIndex).ToString();
        buttonText.text = buttonText.text+":"+LoadTimeString+"\n"+"ストーリー:"+storyDataow+"\n"+"お金:"+money;
        saveLoadButton.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnButtonClicked()
    {
        saveData = new PlayerPrefsSaveData();
        Debug.Log(saveIndex + "のOnEnableが呼ばれました。");
        // !セーブ番号を保存する。これを引用してデータをロードする。
        saveData.SaveLotId(saveIndex);
        // 現在のロードされているセーブ番号を取得する。
        int LoadNowNumber = saveData.LoadNow();
        Debug.Log("現在のセーブナウ番号は" + LoadNowNumber);
        // 現在時間を保存する。
        // 日付け+時間
        string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        saveTime = System.DateTime.Now.Hour * 100 + System.DateTime.Now.Minute;
        // 時間を保存する。
        saveData.SaveTime(saveTime, saveIndex);
        // ロットナンバーでセーブデータを保存する。
        // saveData.JsonSaveToLocal(saveDataJson, saveIndex);
        storyIndex = saveData.JsonSaveToLocal(saveDataJson, saveIndex).storyIndex;
        money = saveDataJson.money;
        // saveData.JsonSaveToLocal(storyIndex, saveIndex);
        // buttonText.text = saveIndex + $"に{storyIndex}保存しましたプレイ時間{now}";
        buttonText.text = now + "\n" + "ストーリー:" + storyIndex+"\n"+"お金:"+money;
        // UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

    }
}
