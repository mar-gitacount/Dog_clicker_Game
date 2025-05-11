using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
public class SaveLoadManager : MonoBehaviour
{
    // 保存対象所持金
    [SerializeField]private Wallet wallet;
    // 犬の頭数
    [SerializeField]Shop shop;

    // 犬データを使う
    [SerializeField]DogDataUse dogdatause;
    // セーブロードインターフェイスフォーマットを変える。
    private ISaveData saveData;
    // クラウド処理もこのスクリプトに書く。


    void Awake()
    {
        saveData = new PlayerPrefsSaveData();
        // !デバッグ用
        // saveData = new DebugSaveData();
    }
    void OnApplicationPause(bool pause)
    {
        Debug.Log("セーブpouse");   

        saveData.SaveMoney(wallet.money);
         SaveToCloud();

    }
    private void OnApplicationQuit()
    {
        Debug.Log("セーブquit");   
        //所持金を保存する。
        saveData.SaveMoney(wallet.money);
        // await saveData.SaveToCloud(); // ← () をつけてメソッドを呼び出す
        // クラウドに保存する。
        SaveToCloud();
        // await saveData.SaveToCloud.data.money = wallet.money.ToString();
        // PlayerPrefs.SetString("MONEY",wallet.money.ToString());
        // 全ての羊の頭数を保存する。
        for (var index = 0; index < shop.dogButtonList.Count; index++)
        {
            var dogButton = shop.dogButtonList[index];
            saveData.SaveDogCnt(index,dogButton.currentCnt);
            // PlayerPrefs.SetInt($"DOG{index}",dogButton.currentCnt);
        } 
    }

    [System.Serializable]
    public class SaveData
    {
        public string money;
        public string username;
        public string password;
        // public Dictionary<string, int> sheepCounts = new Dictionary<string, int>();
    }

     private async Task EnsureUnityServices()
    {
        if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
        {
            await UnityServices.InitializeAsync();
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    // クラウド処理
    public async void SaveToCloud()
    {
        // クラウドセーブの処理をここに書く
        Debug.Log("クラウドセーブの処理を開始します。");
        await EnsureUnityServices();
        
        SaveData data = new SaveData();
        data.money = wallet.money.ToString();
        string json = JsonUtility.ToJson(data);
        var saveData = new Dictionary<string, object>
        {
            { "save_data", json }
        };
        await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
        Debug.Log("クラウドに保存完了");
        Debug.Log("セーブデータ" + json);
        // 例: await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        // 例: await CloudSaveService.Instance.Data.ForceSaveAsync(new Dictionary<string, object> { { "money", wallet.money.ToString() } });
    }
    // クラウドからのロード処理
    public async void LoadFromCloud()
    {
        await EnsureUnityServices();
        // var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new List<string> { "save_data" });
        var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "save_data" });

        if (result.ContainsKey("save_data"))
        {
            var item = result["save_data"];
            string json = item.Value.GetAs<string>();  // 

            Debug.Log("クラウドからロード完了: " + json);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            wallet.money = BigInteger.Parse(loadedData.money);
            Debug.Log("クラウドからロード完了: " + loadedData.money);
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
        LoadFromCloud();
        
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
