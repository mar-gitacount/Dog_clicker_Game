using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using System;
using System.IO;
public class SaveLoadManager : MonoBehaviour
{

    // どのストーリーか、ストーリーか、ストーリー後のゲーム画面か。
    

    // 保存対象所持金
    [SerializeField]private Wallet wallet;
    // 犬の頭数
    [SerializeField]Shop shop;

    // 犬データを使う
    [SerializeField]DogDataUse dogdatause;
    // セーブロードインターフェイスフォーマットを変える。
    private ISaveData saveData;

    int currentSaveSlot; // デフォルトのセーブスロット
    private Dictionary<int, int> savedogDataDict = new Dictionary<int,int>();
    // public  List<SheepCount> sheepCounts = new List<SheepCount>();


    void Awake()
    {
        saveData = new PlayerPrefsSaveData();
        // !デバッグ用
        // saveData = new DebugSaveData();
        currentSaveSlot = saveData.LoadNow();
        if(currentSaveSlot == 0)
        {
            JsonSaveToLocal(new SaveData
            {
                money = wallet.money.ToString(),
                saveIndex = 0

            }, currentSaveSlot);
        }
    }

    public async void saveToLocal()
    {
        
        // saveData.SavenNow(1);
        saveData.LoadNow();

        //所持金を保存する。
        saveData.SaveMoney(wallet.money);
        // 全ての羊の頭数を保存する。
        Debug.Log($"ローカルセーブ処理開始 所持金:{wallet.money}");
        Debug.Log($"{saveData.LoadNow()}はセーブナウの値セーブマネージャー内");
        // SheepCount[] sheepCountsList = new SheepCount[shop.dogButtonList.Count];
        List<SheepCount> sheepCounts = new List<SheepCount>();
        
        // sheepCounts = new List<SheepCount>();
        // 以下で犬が保存されている。どういうロジックかはわからない、、
        for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            Debug.Log($"ローカルセーブ処理 犬インデックス:{index} 頭数:{shop.dogButtonList[index].currentCnt}");
            var dogButton = shop.dogButtonList[index];
            saveData.SaveDogCnt(index, dogButton.currentCnt);
            // 犬データをリストに追加する。
            sheepCounts.Add(new SheepCount
            {
                // Key = $"SHEEP{index}",
                Key = index.ToString(),
                Value = dogButton.currentCnt
            });
            // PlayerPrefs.SetInt($"DOG{index}",dogButton.currentCnt);
        }
        // 適したセーブデータに保存する。
        JsonSaveToLocal(new SaveData
        {
            money = wallet.money.ToString(),
            storyIndex = saveData.LoadStoryProgress(),
            sheepCounts = sheepCounts
            

        }, currentSaveSlot);




        // JsonSaveToLocal(new SaveData
        // {
        //     money = wallet.money.ToString(),
        //     sheepCounts = new List<SheepCount>(sheepCounts),
        //     storyIndex = saveData.LoadStoryProgress()
        // }, currentSaveSlot);
        Debug.Log($"ローカルセーブ処理完了");
    }

    private string GetSavePath(int slot)
    {
        return Application.persistentDataPath + $"/saveData_{slot}.json";
    }

    public void JsonSaveToLocal(SaveData data, int slot)
    {
        data.storyIndex = saveData.LoadStoryProgress();
        string json = JsonUtility.ToJson(data, true);

        Debug.Log($"{data.storyIndex}はjsonのセーブデータ,{data.money}は所持金データ");
        int slotNum = saveData.LoadNow();
        // 犬リスト
        Debug.Log($"{data.sheepCounts}セーブデータ{slot}を保存しました: {GetSavePath(slot)}");
        File.WriteAllText(GetSavePath(slotNum), json);
        foreach (SheepCount sc in data.sheepCounts)
        {
            Debug.Log($"ID: {sc.Key}, Count: {sc.Value}");
        }

    }
    
    public SaveData JsonLoadFromLocal(int slot)
    {
        string path = GetSavePath(slot);
        Debug.Log($"セーブデータパス確認!: {path}");
        int slotNum = saveData.LoadNow();
        Debug.Log($"セーブデータパス: {slotNum}");
        if (File.Exists(path.ToString()))
        {
            
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"セーブデータ{slot}をロードしました: {path}");
            return data;
        }
        else
        {
            Debug.LogWarning($"セーブデータ{slot}が見つかりません: {path}");
            return null;
        }
    }
    void OnApplicationPause(bool pause)
    {
        Debug.Log($"アプリタスクキル時{wallet.money}");

        saveData.SaveMoney(wallet.money);
        // SaveToCloud();
         for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            var dogButton = shop.dogButtonList[index];
            saveData.SaveDogCnt(index,dogButton.currentCnt);
            // PlayerPrefs.SetInt($"DOG{index}",dogButton.currentCnt);
        } 

    }
    private void OnApplicationQuit()
    {
        Debug.Log("セーブquit");   
        //所持金を保存する。
        saveData.SaveMoney(wallet.money);
        // await saveData.SaveToCloud(); // ← () をつけてメソッドを呼び出す
        // クラウドに保存する。
        // SaveToCloud();
        // await saveData.SaveToCloud.data.money = wallet.money.ToString();
        // PlayerPrefs.SetString("MONEY",wallet.money.ToString());
        // 全ての羊の頭数を保存する。
        for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            var dogButton = shop.dogButtonList[index];
            // saveData.SaveDogCnt(index,dogButton.currentCnt);
            // PlayerPrefs.SetInt($"DOG{index}",dogButton.currentCnt);
        } 
    }

    [System.Serializable]
    public class SaveData
    {
        public string money;
        public string username;
        public string password;
        public List<SheepCount> sheepCounts = new List<SheepCount>(); // Listを使用
        public int storyIndex;

        // セーブ番号
        public int saveIndex;
        public int saveTime;
    }

    [System.Serializable]
    public class SheepCount
    {
        public string Key;
        public int Value;
    }

    private async Task EnsureUnityServices()
    {
        try
        {
            if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
            {
                Debug.Log("Unity Servicesを初期化中...");
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Servicesの初期化完了");
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("匿名認証を実行中...");
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("匿名認証成功");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unity Servicesの初期化中にエラーが発生: {ex.Message}");
            throw;
        }
    }
    // クラウド処理
    private bool IsNetworkAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public async void SaveToCloud()
    {
        Debug.Log("クラウドセーブを開始します。");
        if (!IsNetworkAvailable())
        {
            Debug.LogError("ネットワーク接続がありません。クラウドセーブをスキップします。");
            return;
        }

        try
        {
            Debug.Log("クラウドセーブの処理を開始します。");
            await EnsureUnityServices();

            SaveData data = new SaveData();
            data.money = wallet.money.ToString();
            if(shop.dogButtonList.Count != 0)
            {
                Debug.Log("sheepCounts にデータがあります。");
            }
            // 犬の頭数を保存
            for (var index = 0; index < shop.dogButtonList.Count; index++)
            {
                var dogButton = shop.dogButtonList[index];
                data.sheepCounts.Add(new SheepCount { Key = $"SHEEP{index}", Value = dogButton.currentCnt }); // Listにデータを追加
                Debug.Log($"SHEEP{index}: {dogButton.currentCnt} 金{data.money}を保存");
            }
            Debug.Log("犬の頭数チェック:");
            foreach (var sheepCount in data.sheepCounts)
            {
                Debug.Log($"キー: {sheepCount.Key}, 値: {sheepCount.Value}");
            }
            string json = JsonUtility.ToJson(data);
            Debug.Log($"{json}はjsonデータ");
            int dataSize = System.Text.Encoding.UTF8.GetByteCount(json);
            Debug.Log($"JSONデータのサイズ: {dataSize} bytes");
            
            var saveData = new Dictionary<string, object>
            {
                { "save_data", json }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
            Debug.Log("クラウドに保存完了");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"クラウドセーブ中にエラーが発生: {ex.ErrorCode} - {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"予期しないエラーが発生: {ex.Message}");
        }
    }
    // クラウドからのロード処理
    public async void LoadFromCloud()
    {
        await EnsureUnityServices();

        var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "save_data" });

        if (result.ContainsKey("save_data"))
        {
            var item = result["save_data"];
            string json = item.Value.GetAs<string>();

            Debug.Log("クラウドからロード完了: " + json);

            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("クラウドからロード完了: " + loadedData.sheepCounts);

            Debug.Log("クラウドからロード完了: 犬のデータを確認します...");
            Debug.Log($"ロードしたsheepCountsのデータ数: {loadedData.sheepCounts.Count}");
            foreach (var sheepCount in loadedData.sheepCounts)
            {
                Debug.Log($"ロードしたデータ - キー: {sheepCount.Key}, 値: {sheepCount.Value}");
            }

            // 所持金を復元
            wallet.money = BigInteger.Parse(loadedData.money);
            Debug.Log("クラウドからロード完了: 所持金 " + loadedData.money);

            // 犬の頭数を復元
            foreach (var sheepCount in loadedData.sheepCounts)
            {
                Debug.Log($"ロードしたデータ - キー: {sheepCount.Key}, 値: {sheepCount.Value}");

                // キーから犬のIDを取得
                if (int.TryParse(sheepCount.Key.Replace("SHEEP", ""), out int index))
                {
                    if (index < shop.dogButtonList.Count)
                    {
                        var dogButton = shop.dogButtonList[index];
                        dogButton.currentCnt = sheepCount.Value;

                        // 犬を生成
                        for (var i = 0; i < sheepCount.Value; i++)
                        {
                            dogButton.dogGenerator.CreateDog(dogButton.dogdata);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"ロードしたデータのインデックス {index} が範囲外です。");
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("クラウドデータが見つかりませんでした。");
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
        // wallet.money = saveData.LoadMoney();
        // SaveToCloud();
        // 以下android端末だと駄目
        // LoadFromCloud();
        // return;


        // セーブデータをロードする。
        Debug.Log($"現在のセーブスロット:{currentSaveSlot}");
        // json形式で保存されたデータをロードする。
        SaveData loadData = JsonLoadFromLocal(currentSaveSlot);
        // SaveData loadData =JsonUtility.FromJson<SaveData>(JsonUtility.ToJson(JsonLoadFromLocal(currentSaveSlot)));
        Debug.Log($"{loadData.money}はロードデータ");
        wallet.money = BigInteger.Parse(PlayerPrefs.GetString("MONEY", "0"));
        // 全ての犬の頭数をロードする。→ショップオブジェクトから引用している。
        // ?犬データ追加テスト
        for (var index = 0; index < dogdatause.dogButtonList.Count; index++)
        {
            var dogButton = dogdatause.dogButtonList[index];
            var dogCnt = saveData.LoadDogCnt(index);
            dogButton.currentCnt = dogCnt;

            for (var i = 0; i < dogCnt; i++)
            {
                // dogButton.dogGenerator.CreateDog(dogButton.dogdata);
            }
        }
        // !以下をクラウドからのロード処理に変更する。
        for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            // 元犬データ、クラウドにあるわけではなく、ローカルにそのデータがある。

            var dogButton = shop.dogButtonList[index];
            // クラウドの処理にかえる
            // クラウドからのIDをindexで取得する。
            var dogCnt = saveData.LoadDogCnt(index);
            dogButton.currentCnt = dogCnt;
            Debug.Log($"{dogCnt}は犬の数");
            for (var i = 0; i < dogCnt; i++)
            {
                // セーブデータの数だけの犬をメソッド作成する。
                // クラウドからのデータ処理。キーを指定してクラウドのデータとローカルデータを一致させる

                dogButton.dogGenerator.CreateDog(dogButton.dogdata);
            }

        }
        Debug.Log("ロード");
    }
    public void Update()
    {
        // JsonSaveToLocal(new SaveData
        // {
        //     money = wallet.money.ToString(),
        //     storyIndex = saveData.LoadStoryProgress(),
        //     // sheepCounts = sheepCounts
            

        // }, currentSaveSlot);
        saveToLocal();
        Debug.Log($"セーブ所持金:{wallet.money}");

    }

}
