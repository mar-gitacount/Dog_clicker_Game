using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DebugSaveData : ISaveData
{
    public void SaveMoney(BigInteger money)
    {

    }
    public void SaveDogCnt(int id , int cnt)
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BigInteger LoadMoney()
    {
        return 10000000000;
    }
    public int LoadDogCnt(int id)
    {
        return 5;
    }
}
