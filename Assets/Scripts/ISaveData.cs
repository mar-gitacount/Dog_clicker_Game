
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Threading.Tasks;
using System;



public interface ISaveData
{
   
    //所持金のセーブ
    void SaveMoney(BigInteger money);
    //羊頭数のセーブ
    void SaveDogCnt(int id, int cnt);
    // void SaveDogCntdata(int id , int cnt);
    
 
    //所持金のロード
    BigInteger LoadMoney();
    //羊頭数のロード
    int LoadDogCnt(int id);
    // ユーザー名のセーブ
    void UserName(string name);
    // ユーザー名のロード   
    string LoadUserName();
    void password(string password);
    string LoadPassword();

    void SaveStoryProgress(int storyIndex);

    int LoadStoryProgress();
    void SavenNow(int saveIndex,int saveLotId=0);
    int LoadNow();
    void SaveLotId(int saveIndex);
    int LoadLotId(int saveIndex);

    void SaveTime(int time,int slot=0);
    string LoadTime(int slot=0);
    SaveData JsonSaveToLocal(SaveData data,int slot=0);
    SaveData JsonLoadFromLocal(int slot=0);
    // void SaveData(SaveData data);
    // Task SaveToCloud(); // 非同期メソッドに変更
}