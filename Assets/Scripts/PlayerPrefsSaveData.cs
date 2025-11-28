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

    public void SaveDogCntdata(int id, int cnt)
    {
        // Implement the interface method by delegating to the existing SaveDogCnt
        SaveDogCnt(id, cnt);
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
    public int lotId = 0;
    // 現在のセーブ番号を保存セーブ画面を開いたときに呼び出す。
    // ロットナンバーとストーリーを保存する。
    public void SavenNow(int number,int saveLotId=0)
    {
        saveNumber = number;
        Debug.Log("現在のセーブ番号を保存しました。" + saveNumber);
        PlayerPrefs.SetInt($"SAVE_NUMBER", saveNumber);
    }
    // セーブナンバー関数で保存した番号をロードする。
    public int LoadNow()
    {
        saveNumber = PlayerPrefs.GetInt($"SAVE_NUMBER",0); 
        // 存在しない場合は0を返す。
        Debug.Log("現在のセーブ番号をロードしました。" + saveNumber);
        return saveNumber;
    }

    // ロットナンバーをセーブする関数
    public void SaveLotId(int slot=0)
    {
        // 現在のセーブ番号を取得して保存する。
        saveNumber = PlayerPrefs.GetInt($"SAVE_NUMBER",0); 
        // スロットごとにセーブする。
        PlayerPrefs.SetInt($"LOT_ID{slot}", saveNumber);
        Debug.Log("現在のセーブ番号"+saveNumber+"現在のロットナンバーを保存しました。" + slot);
    }

    public int LoadLotId(int slot=0)
    {
        lotId = PlayerPrefs.GetInt($"LOT_ID{slot}",0);
        // !ロットIDがわたされていて、それに対応するセーブ番号をロードする。それをロットIDのみに変える

        Debug.Log("現在のロットナンバーをロードしました。" + lotId);
        return lotId;
    }
    // 時間を保存する
    public void SaveTime(int time,int slot=0)
    {
        string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        // 時間
        // PlayerPrefs.SetInt($"SAVE_TIME{slot}", time);
        // 日付
        PlayerPrefs.SetString($"SAVE_DATETIME{slot}", now);
        Debug.Log(slot + "セーブ時間を保存しました。" + now);
    }
    public string LoadTime(int slot=0)
    {
        string time = PlayerPrefs.GetString($"SAVE_DATETIME{slot}","未保存");
        Debug.Log(slot + "セーブ時間をロードしました。" + time);
        return time;
    }


       private string GetSavePath(int slot)
    {
        return Application.persistentDataPath + $"/saveData_{slot}.json";
    }


    public SaveData JsonSaveToLocal(SaveData data, int slot)
    {
        // データを関数から受け取る。読み込みデータから呼び出して、保存する。

        string json = JsonUtility.ToJson(data, true);
        // 現在のセーブ番号を取得して保存する。
        int slotNum = LoadNow();
        File.WriteAllText(GetSavePath(slot), json);
        Debug.Log($"セーブデータパス: {GetSavePath(slot)}");
        Debug.Log($"{data.sheepCounts}セーブデータ{slot}を保存しました: {GetSavePath(slot)}");
        
        foreach (SheepCount sc in data.sheepCounts)
        {
            Debug.Log($"ID: {sc.Key}, Count: {sc.Value}");
        }
        return data;

    }

    public SaveData JsonLoadFromLocal(int slot=0)
    {
        // 単体の保存セーブに保存する。
        string path = GetSavePath(slot);
        Debug.Log($"ロードしたセーブデータパス確認: {path}");
        int slotNum = LoadNow();
        Debug.Log($"セーブデータパス: {slotNum}");   
        if (File.Exists(path.ToString()))
        {          
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // 所持金をロードして、現段階の所持金に反映する。
            int savemonay = int.Parse(data.money);
            Debug.Log($"ロードした所持金: {savemonay}");
            // SaveMoney(savemonay);
            Debug.Log($"セーブデータ{slot}をロードしました: {path}");
            return data;
        }
        else
        {
            Debug.LogWarning($"セーブデータ{slot}が見つかりません: {path}");
            return null;
        }
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
