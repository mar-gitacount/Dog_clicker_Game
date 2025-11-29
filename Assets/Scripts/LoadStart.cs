using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadStart : MonoBehaviour
{
    // [SerializeField] private LoadButton loadButtonPrefab;
    // Start is called before the first frame update
    [SerializeField] private Text Text;
    [SerializeField] private Button button;
    // [SerializeField] private SaveLoadManager saveLoadManager;
    private ISaveData saveData;
    public int loadIndex;
    public string LoadTimeString;
    public int storyDataow;
    public string TextLabel;
    public SaveData saveDataJson;
    // ループしてボタンを生成する。
    public void SetLabel(string label)
    {
        Text.text = label;
    }
    void Awake()
    {

        // button.onClick.AddListener(OnButtonClicked);
        // button.onClick.AddListener(() => SceneManager.LoadScene("TitleScene"));
    }
    private void OnButtonClicked()
    {
        Debug.Log("クリックされた！");
        saveData = new PlayerPrefsSaveData();
        // ロード番号をテキストから取得する。
        saveData.SavenNow(loadIndex);
        Debug.Log(Text.text + "のLoadStartがクリックされました。");
        // !saveData.SaveMoney(int.Parse(saveData.JsonLoadFromLocal(loadIndex).money));
        // JSONデータを取得して、現在のセーブデータに反映する。
        saveData.LoadDataToCurrentSave(saveDataJson);
        Text.text = "ロードしました。";
        // ロードする番号をセーブマネジメントにする。

        // タイトルへ移動
        // SceneManager.LoadScene("TitleScene");
        // ロード処理をここに追加する。
    }
    void Start()
    {
        // forを回してロードボタンを生成する。
        saveData = new PlayerPrefsSaveData();
        // ロード番号をテキストから取得する。
        loadIndex = int.Parse(Text.text);
        // ロード時間を取得する。
        LoadTimeString = saveData.LoadTime(loadIndex).ToString();
        // int money = int.Parse(saveData.JsonLoadFromLocal(loadIndex).money);
        // JSONデータを取得する。
        saveDataJson = saveData.JsonLoadFromLocal(loadIndex);
        // JSONデータからストーリー番号を取得する。
        storyDataow = saveDataJson.storyIndex;
        // ボタンを押下したら、セーブナウの番号をテキストのデータに保存する。
        // saveData.LoadNow();
        // Text.text = loadIndex.ToString() + ":" + LoadTimeString + "\n" + "ストーリー" + storyDataow;
        TextLabel = loadIndex.ToString() + ":" + LoadTimeString + "\n" + "ストーリー" + storyDataow;;
        Text.text = TextLabel;
        button.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        saveData = new PlayerPrefsSaveData();
        if(saveData.LoadNow() != loadIndex)
        {
            // ボタンのテキストを元に戻す。
            LoadTimeString = saveData.LoadTime(loadIndex).ToString();
            // storyDataow = saveData.LoadStoryProgress();
            // storyDataow = saveData.JsonLoadFromLocal(loadIndex).storyIndex;
            // Text.text = loadIndex.ToString() + ":" + LoadTimeString + "\n" + "ストーリー" + storyDataow;
            Text.text = TextLabel;
        }
    }
}
