
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
 
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
}