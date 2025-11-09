using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

public class PlayerPrefsSaveData : ISaveData
{
    public void SaveMoney(BigInteger money)
    {
        PlayerPrefs.SetString("MONEY", money.ToString());
        Debug.Log($"所持金を保存しました: {money}");
        PlayerPrefs.Save();
    }

    public void SaveDogCnt(int id, int cnt)
    {
        PlayerPrefs.SetInt($"SHEEP{id}", cnt);
        PlayerPrefs.Save();
    }

    public BigInteger LoadMoney()
    {
        Debug.Log($"所持金をロードしました: {PlayerPrefs.GetString("MONEY", "0")}");
        return BigInteger.Parse(PlayerPrefs.GetString("MONEY", "0"));
    }

    public int LoadDogCnt(int id)
    {
        return PlayerPrefs.GetInt($"SHEEP{id}", 0);
    }

    // ストーリー関連のセーブデータ
    // ストーリーとバトルシーン
    public void SaveStoryProgress(int storyIndex)
    {
        Debug.Log($"ストーリー進行度を保存しました: {storyIndex}");
        PlayerPrefs.SetInt("STORY_INDEX", storyIndex);
        PlayerPrefs.Save();
    }
    public int LoadStoryProgress()
    {
        Debug.Log($"ストーリー進行度をロードしました: {PlayerPrefs.GetInt("STORY_INDEX", 0)}");
        return PlayerPrefs.GetInt("STORY_INDEX", 0);
    }

    public void UserName(string name)
    {
        PlayerPrefs.SetString("USERNAME", name);
    }

    public string LoadUserName()
    {
        return PlayerPrefs.GetString("USERNAME", "");
    }

    public void password(string password)
    {
        PlayerPrefs.SetString("PASSWORD", password);
    }

    public string LoadPassword()
    {
        return PlayerPrefs.GetString("PASSWORD", "");
    }
    // 現段階のセーブ番号
    public int saveNumber = 0;
    // 現在のセーブ番号を保存セーブ画面を開いたときに呼び出す。
    public void SavenNow(int number)
    {
        saveNumber = number;
        PlayerPrefs.SetInt("SAVE_NUMBER", saveNumber);
    }
    public int LoadNow()
    {
        saveNumber = PlayerPrefs.GetInt("SAVE_NUMBER", 0);
        return saveNumber;
    }
    // クラウド用のデータ構造
    // [System.Serializable]
    // public class SaveData
    // {
    //     public string money;
    //     public string username;
    //     public string password;
    //     public Dictionary<string, int> sheepCounts = new Dictionary<string, int>();
    // }

    // public async Task SaveToCloud()
    // {
    //     await EnsureUnityServices();

    //     SaveData data = new SaveData();
    //     data.money = LoadMoney().ToString();
    //     data.username = LoadUserName();
    //     data.password = LoadPassword();

    //     // 羊の数を仮に10頭分保存
    //     for (int i = 0; i < 10; i++)
    //     {
    //         data.sheepCounts[$"SHEEP{i}"] = LoadDogCnt(i);
    //     }

    //     string json = JsonUtility.ToJson(data);
    //     var saveData = new Dictionary<string, object>
    //     {
    //         { "save_data", json }
    //    };
    //    // SaveAsyncに渡す前に、適切な型であることを確認
    //    await CloudSaveService.Instance.Data.Player.SaveAsync(saveData);
    //    Debug.Log("クラウドに保存完了");
    // }

    // public async Task LoadFromCloud()
    // {
    //     await EnsureUnityServices();

    //     // var result = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "save_data" });
    //     var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "save_data" });

    //     if (result.TryGetValue("save_data", out var item))
    //     {
    //         // string json = item; // ここで直接代入すればOK
    //         string json = item.Value.GetAsString();
    //         SaveData loaded = JsonUtility.FromJson<SaveData>(json);
    //         SaveMoney(BigInteger.Parse(loaded.money));
    //         UserName(loaded.username);
    //         password(loaded.password);
    //         foreach (var pair in loaded.sheepCounts)
    //         {
    //             int id = int.Parse(pair.Key.Replace("SHEEP", ""));
    //             SaveDogCnt(id, pair.Value);
    //      }

    //     Debug.Log("クラウドからロード完了");
    //     }
        
    //     else
    //     {
    //         Debug.LogWarning("クラウドデータが存在しません");
    //     }
    // }

    // private async Task EnsureUnityServices()
    // {
    //     if (!UnityServices.State.Equals(ServicesInitializationState.Initialized))
    //     {
    //         await UnityServices.InitializeAsync();
    //     }

    //     if (!AuthenticationService.Instance.IsSignedIn)
    //     {
    //         await AuthenticationService.Instance.SignInAnonymouslyAsync();
    //     }
    // }
}
