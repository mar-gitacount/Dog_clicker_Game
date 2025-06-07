using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.CloudSave;

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
    [SerializeField] private DogButton dogbuttonPrefab;
    // 所持金オブジェクト
    [SerializeField] private Wallet wallet;
    [SerializeField] private Button button;
    [SerializeField] private Text GameStartText;
    private ISaveData saveData;
    public void CreateDog()
    {

    }
    void Awake()
    {
        //?テストコード 初期化処理。ドッグボタンプレハブに追加する。
        //?ドッグボタンプレハブ→Walletとdogdataを設定する必要がある。
        foreach (var dogData in dogDatas)
        {
            // 犬ボタンのプレハブ初期化
            var dogButton = Instantiate(dogbuttonPrefab, transform);
            dogButton.dogdata = dogData;
            dogButtonList.Add(dogButton);
            dogButton.dogGenerator = dogGenerator;
            dogButton.wallet = wallet;
        }
        // スタートボタンのイベントリスナーを設定する。

    }
    async void Start()
    {
        saveData = new PlayerPrefsSaveData();
        await InitializeUnityServices();
        button.onClick.AddListener(ChangeScene);
        foreach (var dogData in dogDatas)
        {
            var dogCnt = 10;
            for (var i = 0; i < dogCnt; i++)
            {
                dogGenerator.CreateDog(dogData);

            }

        }
    }
    async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services 初期化完了");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"初期化エラー: {ex.Message}");
        }
    }

    // ゲームシーンへチェンジする関数。
    async public void ChangeScene()
    {
        button.interactable = false;
        var loadingTask = ShowLoadingAnimation();

        // すでにサインイン済みかチェック
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            if (saveData.LoadUserName() != "")
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(saveData.LoadUserName(), saveData.LoadPassword());
                Debug.Log($"ユーザー名: {saveData.LoadUserName()}");
                Debug.Log($"パスワード: {saveData.LoadPassword()}");
                SceneManager.LoadScene("MenuScene");
                return;
            }
            else
            {
                // ログインしてない場合、ログイン画面遷移
                SceneManager.LoadScene("LoginOrSinUpScene 1");
                return;
            }
        }

        // サインイン済みならそのままメニューシーンへ
        SceneManager.LoadScene("MenuScene");
    }
    // ロード中のアニメーション
    async Task ShowLoadingAnimation()
    {
        string[] dots = { ".", "..", "..." };
        int index = 0;
        while (true)
        {
            GameStartText.text = "ロード中" + dots[index];
            index = (index + 1) % dots.Length;
            await Task.Delay(500); // 0.5秒待機
        }
    }


}