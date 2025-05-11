
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Threading.Tasks;

 
public interface ISaveData
{
    //所持金のセーブ
    void SaveMoney(BigInteger money);
    //羊頭数のセーブ
    void SaveDogCnt(int id, int cnt);
 
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
    // void SaveData(SaveData data);
    // Task SaveToCloud(); // 非同期メソッドに変更
}